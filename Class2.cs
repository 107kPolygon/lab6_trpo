using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_trpo
{
    [TestFixture]
    class Class2
    {
        IWebDriver webDriver = new ChromeDriver();
        [TestCase]
        public void mainTitle() //1. Проверить заголовок страницы
        {
            webDriver.Url = "https://sibsutis.ru/";
            //установка периода ожидания для веб драйвера (30 секунд)
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            //установка условия окончания ожидания
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = webDriver.FindElement(By.XPath("//*[@id=\"burger\"]/span[3]"));
                    return elementToBeDisplayed.Displayed;
                }
                //в случае, если такого элемента нет
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            Assert.AreEqual("Сибирский государственный университет телекоммуникаций и информатики", webDriver.Title);
            //webDriver.Close();
        }
        [TestCase]
        public void DisplayEl()//2. Проверка видимости объектов
        {
            webDriver.Url = "https://sibsutis.ru/";
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            var element = wait.Until(condition =>
            {
                try
                {
                    var el = webDriver.FindElement(By.XPath("/html/body/div[3]/header/div/a"));
                    return el.Displayed;
                                        
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            IWebElement el1 = webDriver.FindElement(By.XPath("/html/body/div[3]/header/div/a"));
            bool status = el1.Displayed; //проверяет, отображается ли элемент на веб странице
            Assert.True(status);
            //webDriver.Close();
        }

        [TestCase]
        public void PerehodLink() //3. Переход по ссылке
        {
            webDriver.Url = "https://sibsutis.ru/";
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            var element = wait.Until(condition =>
            {
                try
                {
                    var znak0 = webDriver.FindElement(By.XPath("//*[@id=\"layout\"]/header/nav[2]/div[1]/a/img"));
                    //var element0 = webDriver.FindElement(By.XPath("/html/body/div[3]/header/nav[2]/div[1]/form/input"));
                    return znak0.Displayed;
                    
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            IWebElement znak = webDriver.FindElement(By.XPath("//*[@id=\"layout\"]/header/nav[2]/div[1]/a/img"));
            znak.Click();
            IWebElement element1 = webDriver.FindElement(By.XPath("/html/body/div[3]/header/nav[2]/div[1]/form/input"));
            element1.SendKeys("сессия");
            element1.SendKeys(Keys.Return);//чтобы запрос принялся
            Assert.AreEqual("https://sibsutis.ru/search/?q=%D1%81%D0%B5%D1%81%D1%81%D0%B8%D1%8F", webDriver.Url);
            //Assert.IsTrue(webDriver.PageSource.Contains("Доступно расписание лабораторно - экзаменационной сессии"));
            //webDriver.Close();
        }

        [TestCase]
        public void Textbox() //4. Заполнение текстового поля
        {
            webDriver.Url = "https://sibsutis.ru/students/";

            IWebElement search = webDriver.FindElement(By.XPath("/html/body/center/div[2]/div[2]/div[2]/div[2]/div[3]/form/input[1]"));//ссылка на строку поиска
            search.SendKeys("цукенг");//ввод запроса
            IWebElement button = webDriver.FindElement(By.XPath("/html/body/center/div[2]/div[2]/div[2]/div[2]/div[3]/form/input[2]"));//ссылка на кнопку найти
            button.Click();
            Assert.IsTrue(webDriver.PageSource.Contains("К сожалению, на ваш поисковый запрос ничего не найдено."));//есть ли такое содержимое на сайте
           // webDriver.Close();
        }

        [TestCase]//эмуляция нажатия на кнопку
        public void Button()
        {
            webDriver.Url = "https://sibsutis.ru/";
            var wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            var element = wait.Until(condition =>
            {
                try
                {
                    var button0 = webDriver.FindElement(By.XPath("//*[@id=\"layout\"]/header/nav[1]/a[2]"));
                    return button0.Displayed;    
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
            IWebElement button = webDriver.FindElement(By.XPath("//*[@id=\"layout\"]/header/nav[1]/a[2]"));
            button.Click();
            Assert.IsTrue(webDriver.Url == "https://sibsutis.ru/students/");
        }

        [TearDown]
        public void TestEnd()
        {
            webDriver.Quit();

        }
    }
}
