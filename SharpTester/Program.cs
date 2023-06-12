using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace EdgeDriverSample
{
    class Program
    {
        static EdgeDriver driver = new EdgeDriver();
        static Dictionary<string, string> XPATH = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            XPATH.Add("Variant", "//*[@id=\"answer_1_id\"]");
            XPATH.Add("Next", "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
            XPATH.Add("Exit", "//*[@id=\"root\"]/div/div[1]/div/ul/li[3]/a");
            XPATH.Add("Gray Next", "//*[@id=\"root\"]/div/div[2]/div/div[2]/div/div[2]/button");
            driver.Url = "http://miq.exam.edu.az";
            int a = Convert.ToInt32(args[0]);
            for (int c = 1; c <= a; c++)
            {

            }
            FillInForum(driver, "22003", "0522");
            Thread.Sleep(2000);
            ClickButton(driver, XPATH["Gray Next"]);
            Thread.Sleep(2000);
            //Variant LOOP.
            for (int i = 1; i <= 60; i++)
            {
                //SWEETSPOT 180
                Console.WriteLine(i);
                Thread.Sleep(180);
                ClickButton(driver: driver, XPATH["Variant"]); //WORKS
                Thread.Sleep(180);
                if(i == 1)
                {
                    ClickButton(driver: driver, "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button");
                }
                else
                {
                    ClickButton(driver: driver, $"//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
                }    
                Thread.Sleep(180);
            }
            ClickButton(driver: driver, XPATH["Variant"]);
            Thread.Sleep(180);
            ClickButton(driver: driver, XPATH["Next"]);
            Thread.Sleep(180);
            IWebElement element = driver.FindElement(By.Id("email"));
            Thread.Sleep(180);
            element.SendKeys($"");
            Thread.Sleep(180);
            ClickButton(driver: driver, XPATH["Finish"]);   
        }

        private static void FillInForum(EdgeDriver driver, string log, string pass)
        {
            try
            {
                IWebElement element = driver.FindElement(By.Id("email"));
                element.SendKeys(log);
                element = driver.FindElement(By.Id("password"));
                element.SendKeys(pass);
                element.Submit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ClickButton(EdgeDriver driver, string identifier)
        {
            try
            {
                IWebElement element = driver.FindElement(By.XPath(identifier));
                element.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //driver.Quit();
            }
        }


        // LOGOUT  - text - çıxış
        // Answer - id - answer_1_id goes from 1 to 5. Will always select 1
        // Davam Et - XPATH - //*[@id="root"]/div/div[2]/div/div[2]/div/div[2]/button
        // Next - XPATH - //*[@id="root"]/div/div[2]/div[4]/div/div[2]/div[2]/button
        // Variant A - XPATH - //*[@id="answer_1_id"]
        // FIN - ID - email
        // FIN Button - /html/body/div[4]/div/div[1]/div/div/div[2]/div[2]/div/button
    }
}