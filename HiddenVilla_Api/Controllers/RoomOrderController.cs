﻿using Business.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomOrderController : Controller
    {
        private readonly IRoomOrderDetailsRepository _repository;
        private readonly IEmailSender _emailSender;

        public RoomOrderController(IRoomOrderDetailsRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO details)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Create(details);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating Room details/booking"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PaymentSuccessful([FromBody] RoomOrderDetailsDTO details)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(details.StripeSessionId);
            if(sessionDetails.PaymentStatus == "paid")
            {
                var result = await _repository.MarkPaymentSuccessful(details.Id);
                if(result == null)
                {
                    return BadRequest(new ErrorModel()
                    {
                        ErrorMessage = "Can't mark payment as successful"
                    });
                }
                await _emailSender.SendEmailAsync(details.Email, "Booking Confirmed - HiddenVilla",
                    "Your Booking has been confirmed at HiddenVilla with Order Id: " + details.Id);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Can't mark payment as successful"
                });
            }
        }
    }
}
