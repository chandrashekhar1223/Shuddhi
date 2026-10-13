using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text;
using static System.Net.WebRequestMethods;


namespace Shuddhi.Controllers
{
    public class OTPGeneratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/OTP/otpgenerator")]
        public async Task<IActionResult> SendSms()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    Console.WriteLine(port);
                }



                SendSMS("7385572258", "12345");

                using HttpClient client = new HttpClient();

                var json = @"{
            ""phone"":""9322945487"",
            ""message"":""Your OTP is 123456""
        }";

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync("http://192.168.0.117:8080/send", content);

                string result = await response.Content.ReadAsStringAsync();

                return Ok(new
                {
                    StatusCode = response.StatusCode,
                    Response = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        public void SendSMS(string mobile, string otp)
        {
            SerialPort port = new SerialPort("COM3", 9600);

            port.Open();

            port.WriteLine("AT");
            port.WriteLine("AT+CMGF=1");
            port.WriteLine($"AT+CMGS=\"{mobile}\"");

            port.Write("Your OTP is " + otp);

            port.Write(new char[] { (char)26 }, 0, 1);

            port.Close();
        }
    }
}
