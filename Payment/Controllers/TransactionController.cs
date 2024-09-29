using Application.Commands;
using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TransactionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        // Pay Transaction
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] CreateTransactionDto createTransactionDto)
        {
            if (createTransactionDto == null)
            {
                return BadRequest("Invalid transaction data.");
            }
            try
            {
                var command = _mapper.Map<CreateTransactionCommand>(createTransactionDto);
                var result = await _mediator.Send(command);
                var transactionDto = _mapper.Map<TransactionDto>(result);
                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // Cancel Transaction
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelTransactionDto cancelTransactionDto)
        {
            if (cancelTransactionDto == null)
            {
                return BadRequest("Invalid cancel request.");
            }
            try
            {
                var command = _mapper.Map<CancelTransactionCommand>(cancelTransactionDto);
                var result = await _mediator.Send(command);
                var transactionDto = _mapper.Map<TransactionDto>(result);
                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // Refund Transaction
        [HttpPost("refund")]
        public async Task<IActionResult> Refund([FromBody] RefundTransactionDto refundTransactionDto)
        {
            if (refundTransactionDto == null)
            {
                return BadRequest("Invalid refund request.");
            }
            try
            {
                var command = _mapper.Map<RefundTransactionCommand>(refundTransactionDto);
                var result = await _mediator.Send(command);
                var transactionDto = _mapper.Map<TransactionDto>(result);
                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // Get Transaction by Id
        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransactionById(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest("Invalid transaction ID.");
            }
            try
            {
                var result = await _mediator.Send(new GetTransactionByIdQuery(transactionId));
                if (result == null)
                {
                    return NotFound();
                }
                var transactionDto = _mapper.Map<TransactionDto>(result);
                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // Report: Get Transactions with filters
        [HttpGet("report")]
        public async Task<IActionResult> GetTransactions([FromQuery] ReportQueryDto reportQueryDto)
        {
            try
            {
                var query = _mapper.Map<ReportQuery>(reportQueryDto);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}