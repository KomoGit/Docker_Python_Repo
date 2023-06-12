﻿using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace EdgeDriverSample 
{
    class Program
    {
        static readonly EdgeDriver Driver = new();
        static readonly Dictionary<string, string> xPathMap = new();
        static int totalExceptionCount = 0;

        //Sweetspot 180 ms
        const int THREAD_CONTROLLER = 500; //ms.
        static void Main(string[] args)
        {            
            const int USER_RANGE_START = 22025;
            const int USER_RANGE_END = 22126;
            const string USER_PASS = "0522";

            #region xPath map
            xPathMap.Add("Answer", "//*[@id=\"answer_1_id\"]");
            xPathMap.Add("Next", "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
            xPathMap.Add("Logout", "//*[@id=\"root\"]/div/div[1]/div/ul/li[3]/a");
            xPathMap.Add("FINInput", "/html/body/div[3]/div/div[1]/div/div/div[2]/div[1]/div/input");
            xPathMap.Add("FINSubmitButton", "/html/body/div[3]/div/div[1]/div/div/div[2]/div[2]/div/button");
            #endregion

            Driver.Url = "INSERT URL";// <- CHANGE ME
            for (int userIndex = USER_RANGE_START; userIndex <= USER_RANGE_END; userIndex++)
            {
                FillInLogin(Driver, $"{userIndex}", USER_PASS);
                Thread.Sleep(2000);
                ClickButton(Driver, xPathMap["Next"]);
                Thread.Sleep(2000);
                //ANSWERS Loop
                for (int i = 1; i <= 60; i++)
                {
                    Console.WriteLine($"Question No: {i}");
                    Thread.Sleep(THREAD_CONTROLLER);
                    ClickButton(driver: Driver, xPathMap["Answer"]);
                    Thread.Sleep(THREAD_CONTROLLER);
                    if (i == 1)
                    {
                        ClickButton(driver: Driver, "//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button");
                    }
                    else
                    {
                        ClickButton(driver: Driver, $"//*[@id=\"root\"]/div/div[2]/div[4]/div/div[2]/div[2]/button[2]");
                    }
                    Thread.Sleep(THREAD_CONTROLLER);
                }
                ClickButton(driver: Driver, xPathMap["Answer"]);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(driver: Driver, xPathMap["Next"]);
                Thread.Sleep(THREAD_CONTROLLER);
                FillInFIN(userIndex);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(Driver, xPathMap["FINSubmitButton"]);
                Thread.Sleep(THREAD_CONTROLLER);
                ClickButton(Driver, xPathMap["Logout"]);
                Thread.Sleep(THREAD_CONTROLLER);
                Console.WriteLine($"User {userIndex} finished with {totalExceptionCount}");
            }  
        }

        private static void FillInLogin(EdgeDriver driver, string login, string paswd)
        {
            try
            {
                IWebElement element = driver.FindElement(By.Id("email"));
                element.SendKeys(login);
                element = driver.FindElement(By.Id("password"));
                element.SendKeys(paswd);
                element.Submit();
            }
            catch (Exception ex)
            {
                totalExceptionCount++;
                Console.WriteLine(ex.Message);
            }
        }

        private static void FillInFIN(int a)
        {
            try
            {
                IWebElement element = Driver.FindElement(By.XPath(xPathMap["FINInput"]));
                /*
                !!! MAJOR CHANGE !!!
                While the element isn't enabled, the thread will be put to sleep for x amount of time
                Once it becomes enabled the while loop breaks and element.SendKeys works. 
                */
                while (!element.Enabled)//IF while works we can get rid of other Thread.Sleep() Calls.
                {
                    Thread.Sleep(THREAD_CONTROLLER);
                    //Incase the loop goes infinitely, use break.
                    //if (element.Enabled) break; <-
                }
                element.SendKeys(a.ToString());
            }
            catch (Exception)
            {
                totalExceptionCount++;
                Console.WriteLine("Error! Could not fill FIN. Perhaps text field is unresponsive?");
            }      
        }

        private static void ClickButton(EdgeDriver driver, string identifier)
        {
            try
            {
                /*
                !!! MAJOR CHANGE !!!
                While the element isn't enabled, the thread will be put to sleep for x amount of time
                Once it becomes enabled the while loop breaks and element.SendKeys works. 
                */
                IWebElement element = driver.FindElement(By.XPath(identifier));
                while (!element.Enabled)
                {
                    Thread.Sleep(THREAD_CONTROLLER);
                    //if (element.Enabled) break; <-
                    //Incase the loop goes infinitely, use break.
                }
                element.Click();
            }
            catch (Exception)
            {
                totalExceptionCount++;
                Console.WriteLine("MALFUNCTION 54");
            }
        }
    }
}