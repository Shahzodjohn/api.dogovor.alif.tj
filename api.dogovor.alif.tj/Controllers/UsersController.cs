using Entity.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.UserService;
using System.Security.Claims;
using Entity;
using GemBox.Document;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using SautinSoft;
using System.Diagnostics;
using RestSharp;
using ConvertApiDotNet;

namespace api.dogovor.alif.tj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UsersController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var returnMessage = await _userService.RegisterUser(dto);
            if (returnMessage.Status == "400")
                return BadRequest(returnMessage);
            else
                return Ok(returnMessage);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthorizationDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var returnMessage = await _userService.Login(dto);
            if (returnMessage.Status == "400")
                return BadRequest(returnMessage);
            return Ok("Json web token = " + returnMessage.Message);
        }
        [HttpGet("CurrentUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CurrentUser()
        {
            var claim = User.Identity as ClaimsIdentity;
            if (claim == null) return BadRequest(400);
            var userInfo = await _userService.UsersInformation(claim);
            return Ok(userInfo);
        }
 
        //[HttpPost]
        //public async Task<IActionResult> ConvertInto([FromForm] ConvertCredentials credentials)
        //{
        //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //    var path = (Path.Combine(_environment.WebRootPath, $"{ DateTime.Today.ToString("D") }"));
        //    var txtfile = (Path.Combine(path, "htmlText.txt"));


        //    if (!System.IO.Directory.Exists(path))
        //        System.IO.Directory.CreateDirectory(path);

        //    using (System.IO.StreamWriter writer = System.IO.File.AppendText(txtfile))
        //    {
        //        writer.Write(credentials.Text);
        //    }

            
        //    #region
        //    //ChromeOptions options = new ChromeOptions();
        //    //options.AddArguments("--headless");
        //    //options.AddArguments("--disable-infobars");
        //    //options.AddArguments("--start-maximized");
        //    //options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

        //    //IWebDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
        //    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        //    //driver.Navigate().GoToUrl(@"D:\repos\api.dogovor.alif.tj\api.dogovor.alif.tj\customwwwroot\13 июля 2022 г\htmlText.html");
        //    //// Actions actions = new Actions(driver);
        //    //Actions actions = new Actions(driver);
        //    ////  ((IJavaScriptExecutor)driver).ExecuteScript("window.print();");
        //    //((IJavaScriptExecutor)driver).ExecuteScript("setTimeout(function() { window.print(); }, 0);");
        //    //Thread.Sleep(1000);
        //    ////driver.FindElement(By.XPath("//*[@id='sidebar']//print-preview-button-strip//div/cr-button[2]/text()")).Click();
        //    //driver.SwitchTo().Window(driver.WindowHandles.Last());
        //    //driver.FindElement(By.XPath("/html")).SendKeys(Keys.Enter);
        //    ////for (int i = 0; i < 50; i++)
        //    ////{
        //    ////    driver.FindElement(By.XPath("/html")).SendKeys(Keys.Tab);
        //    ////}


        //    ////((IJavaScriptExecutor)driver).ExecuteScript("window.print=function(){};");




        //    ////Robot robot = new Robot();
        //    ////robot.keyPress(KeyEvent.VK_CONTROL);
        //    ////robot.keyPress(KeyEvent.VK_P);
        //    ////robot.keyRelease(KeyEvent.VK_P);
        //    ////robot.keyRelease(KeyEvent.VK_CONTROL);

        //    ////// get current browser window handles and switch to window with handle that is last in the list
        //    ////Set<String> windowHandles = driver.getWindowHandles();
        //    ////for (String handle : windowHandles)
        //    ////{
        //    ////    driver.switchTo().window(handle);
        //    ////}

        //    ////driver.findElement(By.xpath("//button[contains(text(), 'Change')]")).click();
        //    ////driver.findElement(By.xpath("//span[contains(text(), 'Save as PDF')]")).click();
        //    ////driver.findElement(By.xpath("//button[contains(text(), 'Save')]")).click();

        //    ////// you might need to add waiter here that waits a second, since script is too fast
        //    ////// and needs to wait for save dialog box to be shown

        //    ////robot.keyPress(KeyEvent.VK_ENTER);
        //    ////ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
        //    ////driver = new ChromeDriver(chromeDriverService, options);

        //    ////var downloadsPath = path;
        //    ////var generatedFilePngs = Directory.GetFiles(downloadsPath, string.Format("{0}*.pdf", "TheNameOfYourPDF"));




        //    ////IWebElement element = driver.FindElement(By.XPath("/html/body/div[2]/div[1]"));
        //    ////actions actions = new actions(driver);
        //    ////actions.sendkeys(openqa.selenium.keys.f12).perform();
        //    ////IAction sendLowercase = actions.SendKeys(Keys.Control + "p" + Keys.Control).Build();
        //    ////sendLowercase.Perform();




        //    ////System.Diagnostics.Process process = new System.Diagnostics.Process();
        //    ////process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    ////process.StartInfo.FileName = @"c:\program files\google\chrome\application\chrome.exe";

        //    ////string arguments = $@"--headless --disable-gpu --pt --print-to-pdf=""{Path.Combine(path, "output.pdf")}"" ""{txtfile.Replace("txt", "html")}""";
        //    //////process.StartInfo.Verb = "Print";
        //    ////process.StartInfo.Arguments = arguments;
        //    ////process.Start();




        //    #endregion
        //    return Ok();
        //}
        [HttpPost("Upload a new Contract")]
        public async Task<IActionResult> ConvertingTextIntoRtf(IFormFile form)
        {
            var path = (Path.Combine(_environment.WebRootPath, $"{ DateTime.Today.ToString("D") }"));
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string filePath = Path.Combine(path, form.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                form.CopyTo(fileStream);

            var convertApi = new ConvertApi("S1alNMap0GwMC3zi");
            var convert = await convertApi.ConvertAsync("docx", "rtf",
                new ConvertApiFileParam("File", filePath)
            );

            var rtfFile = filePath.Replace("docx", "rtf");
            var textFile = filePath.Replace("docx", "txt");
            await convert.SaveFilesAsync(path);
            if(!System.IO.File.Exists(textFile))
                System.IO.File.Move(rtfFile, Path.ChangeExtension(rtfFile, ".txt"));
            else if(System.IO.File.Exists(textFile))
                System.IO.File.Delete(textFile);
            var finaltext = System.IO.File.ReadAllText(textFile);
            

            string text = "sdads";
            

             
            return Ok();
        }
        
       
    }
}
