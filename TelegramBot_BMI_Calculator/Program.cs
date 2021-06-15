using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramBot_BMI_Calculator.Data;
using TelegramBot_BMI_Calculator.Models;

namespace TelegramBot_BMI_Calculator
{

    class Program
    {
        private static TelegramBotClient client = null;
        static void Main(string[] args)
        {
            Console.WriteLine("Telegram bot started!");

            client = new TelegramBotClient("1717731472:AAE5DO6NU2XfY2qFrZ1CuDp5KpY3ImDsuAM");
            client.OnMessage += Client_OnMessage;

            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();

        }


/*
        private static int stack;
        private static string s1 = "";
        private static string s2 = "";
        private static string s3 = "";
        private static string s4 = "";*/
        //Mine funksion

        private static string myFuncStart(string _id)
        {
            try
            {
                using(ApplicationDbContext context = new ApplicationDbContext())
                {
                    var v = (from a in context.MyTests
                             where a.UserId == _id
                             select a).FirstOrDefault();
                    if(v != null)
                    {
                        context.MyTests.Remove(v);
                        context.SaveChanges();
                    }

                    myTest m = new myTest()
                    {
                        Age = 0,
                        Gender = "",
                        Height = 0,
                        step = 1,
                        UserId = _id,
                        Weight = 0
                    };
                    context.MyTests.Add(m);
                    context.SaveChanges();
                    return "Yoshingizni kiriting";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }

        private static string myFunc(string s, string _id)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var v = (from a in context.MyTests
                             where a.UserId == _id
                             select a).FirstOrDefault();

                    if (v.step == 1)
                    {
                        try
                        {
                            if (Convert.ToInt32(s) > 0)
                            {
                                v.Age = Convert.ToInt32(s);
                                v.step = 2;
                                context.SaveChanges();
                                return "Vazningizni kiriting(KG)";
                            }
                            else
                            {
                                return "Yoshingizni qaytatdan kiriting!";
                            }

                        }
                        catch (Exception ex)
                        {
                            return "Yoshingizni qaytatdan kiriting!";
                        }

                    }
                    else if (v.step == 2)
                    {
                        try
                        {
                            if (Convert.ToDouble(s) > 0)
                            {
                                v.Weight = Convert.ToInt32(s);
                                v.step = 3;
                                context.SaveChanges();
                                return "Bo'yingizni kiriting(SM)";
                            }
                            else
                            {
                                return "Vazningizni qaytatdan kiriting!(KG)";
                            }

                        }
                        catch (Exception ex)
                        {
                            return "Vazningizni qaytatdan kiriting!(KG)";
                        }
                    }
                    else if (v.step == 3)
                    {
                        try
                        {
                            if (Convert.ToInt32(s) > 0)
                            {
                                v.Height = Convert.ToInt32(s);
                                v.step = 4;
                                context.SaveChanges();
                                return "Jinsingizni kiriting(Erkak/Ayol)";
                            }
                            else
                            {
                                return "Bo'yingizni qaytatdan kiriting!(SM)";
                            }

                        }
                        catch (Exception ex)
                        {
                            return "Bo'yingizni qaytatdan kiriting!(SM)";
                        }
                    }
                    else
                    {
                        try
                        {
                            if (s.ToUpper() == "ERKAK" || s.ToUpper() == "AYOL")
                            {
                                v.Gender = s.ToUpper();
                                context.SaveChanges();
                                string text = myMess(_id);

                                return text;
                            }
                            else
                            {
                                return "Jinsingizni qaytatdan kiriting!(Erkak/Ayol)";
                            }
                        }
                        catch (Exception ex)
                        {
                            return "Jinsingizni qaytatdan kiriting!(Erkak/Ayol)";
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private static string myMess(string str)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var v = (from a in context.MyTests
                             where a.UserId == str
                             select a).FirstOrDefault();

                    double bmi = Convert.ToDouble(v.Weight) / ((Convert.ToDouble(v.Height) / 100) * (Convert.ToDouble(v.Height) / 100));
                    string text = "";
                    if (bmi < 18.5)
                    {
                        text = $"Sizning BMI(Body Mass Index) {String.Format("{0:0.00}", bmi)}. Bu sizning tanangizda vazn yetishmasligini bildiradi.\n";
                    }
                    else if (bmi >= 18.5 && bmi < 25)
                    {
                        text = $"Sizning BMI(Body Mass Index) {String.Format("{0:0.00}", bmi)}. Bu NORMAL xolat.\n";
                    }
                    else if (bmi >= 25 && bmi < 30)
                    {
                        text = $"Sizning BMI(Body Mass Index) {String.Format("{0:0.00}", bmi)}. Bu sizning tanangizda ortiqcha vazn borligini anglatadi.\n";
                    }
                    else if (bmi >= 30 && bmi < 35)
                    {
                        text = $"Sizning BMI(Body Mass Index) {String.Format("{0:0.00}", bmi)}. Bu sizning tanangizda 1-darajali semirish borligini anglatadi.\n";
                    }
                    else if (bmi >= 35 && bmi < 40)
                    {
                        text = $"Sizning BMI(Body Mass Index) {String.Format("{0:0.00}", bmi)}. Bu sizning tanangizda 2-darajali semirish borligini anglatadi.\n";
                    }
                    else
                    {
                        text = $"Sizning BMI(Body Mass Index) {String.Format("{0:0.00}", bmi)}. Bu sizning tanangizda 3-darajali semirish borligini anglatadi.\n";
                    }
                    text += "Ma'lumotlarni yangidan kiritish uchun /start ni bosing!";

                    BMI b = new BMI()
                    {
                        Gender = v.Gender,
                        Age = v.Age,
                        Height = v.Height,
                        Weight = v.Weight
                    };
                    context.BMIs.Add(b);
                    context.SaveChanges();
                    return text;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        private static void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var v = e.Message.Text;
            if(v == "/start")
            {
                client.SendTextMessageAsync(e.Message.Chat.Id, myFuncStart(e.Message.Chat.Id.ToString()));
            }
            else
            {
                client.SendTextMessageAsync(e.Message.Chat.Id, myFunc(v, e.Message.Chat.Id.ToString()));
            }


            
        }
    }
}
