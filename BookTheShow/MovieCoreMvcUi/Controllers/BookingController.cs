﻿using BookTheShowEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingCoreMvcUi.Controllers
{
    public class BookingController : Controller
    {
        IConfiguration _configuration;
        public BookingController(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index(Booking booking)
        {
            IEnumerable<Booking> bookingresult = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Bookingg/Getbooking";//api controller name and httppost name given inside httppost in Bookingcontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        bookingresult = JsonConvert.DeserializeObject<IEnumerable<Booking>>(result);
                    }



                }
            }
            return View(bookingresult);
        }
        public IActionResult BookingEntry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BookingEntry(Booking booking)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(booking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Bookingg/AddBooking";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Booking Details Saved Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }

                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> EditBooking(int BookingId)
        {
            Booking Booking = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Bookingg/GetBookingById?bookingId=" + BookingId;//BookingId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Bookingcontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        Booking = JsonConvert.DeserializeObject<Booking>(result);
                    }



                }
            }
            return View(Booking);

        }
        [HttpPost]
        public async Task<IActionResult> EditBooking(Booking Bookingv)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Bookingv), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Bookingg/UpdateBooking";//api controller name and its function

                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Bookings Details Updated Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }

                }
            }
            return View(Bookingv);


        }
        public async Task<IActionResult> DeleteBooking(int BookingId)
        {
            Booking Booking = null;
            using (HttpClient client = new HttpClient())
            {


                string endPoint = _configuration["WebApiBaseUrl"] + "Bookingg/GetBookingById?bookingId=" + BookingId;//BookingId is apicontroleer passing argument name//api controller name and httppost name given inside httppost in Bookingcontroller of api

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        Booking = JsonConvert.DeserializeObject<Booking>(result);
                    }



                }
            }
            return View(Booking);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteBooking(Booking Bookingv)
        {
            ViewBag.status = "";
            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {

                string endPoint = _configuration["WebApiBaseUrl"] + "Bookingg/DeleteBooking?bookingId=" + Bookingv.BookingId;  //api controller name and its function

                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "Bookings Details Deleted Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";
                    }

                }
            }
            return View(Bookingv);





        }

    }
}
