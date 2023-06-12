using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading.Tasks.Sources;

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
            const int THREAD_CONTROLLER = 500;
            int a = 22025;
            int b = 22126;
            for (int c = a; c <= b; c++)
            {
                FillInForum(driver, $"{c}", "0522");
                Thread.Sleep(2000);
                ClickButton(driver, XPATH["Gray Next"]);
                Thread.Sleep(2000);
                //Variant LOOP.
                for (int i = 1; i <= 60; i++)
                {
                    //SWEETSPOT 180
                    Console.WriteLine(i);
                    Thread.Sleep(THREAD_CONTROLLER);
                    ClickButton(driver: driver, XPATH["Variant"]); //WORKS
                    Thread.Sleep(THREAD_CONTROLLER);
                    if (i == 1)
                    {
                        ClickButton(driver: driver, "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button");
                    }
                    else
                    {
                        ClickButton(driver: driver, $"//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
                    }
                    Thread.Sleep(THREAD_CONTROLLER);
                }
                ClickButton(driver: driver, XPATH["Variant"]);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(driver: driver, XPATH["Next"]);
                Thread.Sleep(THREAD_CONTROLLER);
                FillInFin(c);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(driver, "/html/body/div[3]/div/div[1]/div/div/div[2]/div[2]/div/button");
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(driver, XPATH["Exit"]);
                Thread.Sleep(THREAD_CONTROLLER);
            }  
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

        private static void FillInFin(int a)
        {
            try
            {
                //IWebElement element = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
                IWebElement element = driver.FindElement(By.XPath("/html/body/div[3]/div/div[1]/div/div/div[2]/div[1]/div/input"));
                element.SendKeys(a.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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