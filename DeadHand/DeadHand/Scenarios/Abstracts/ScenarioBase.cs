using DeadHand.Commands.Implementations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace DeadHand.Scenarios.Abstracts
{
    internal abstract class ScenarioBase
    {
        public abstract string ScenarioName { get; }
        protected Random _rng;
        protected EmailCommand _emailService;
        protected List<System.Timers.Timer> _triggers;
        public abstract void StartScenario();

        protected void SendShutdownKeyMessage_Elapsed(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "command@stratcom.com",
                Subject = "Order no. 584",
                ReceivedDate = DateTime.Now,
                Content = $"Asset {Environment.UserName},"+
                @"The hostile government has issued stand down order to its military forces, and expressed will to resume peace talks.
Our intelligence has noted, that hostile forces are indeed standing down and returning to their bases."+
$"With that in mind, Startegic Command authorizes {Environment.UserName} to enter following Dead Hand shutdown code IMMEDIATELY" +
@"FA7S-I82B-HEY4-HWEF"

            }, false);

            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendPresidentialAddress_Elapsed(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "PRESIDENTIAL ADDRESS",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF THE PRESIDENT. THIS IS NOT A TEST

STANDBY FOR PRESIDENTIAL ADDRESS...

Ladies and gentlemen, the President of our Country

My fellow citzens,
I am standing before you in this hour of test. 
Our military forces had reported activities of enemy air forces, fleet and strategic weapon divisions, that suggest threat to our sovergnity.
We are especially concerned, about the possibility of nuclear attack. 

However we are still trying to resume peace talks in Geneva, that had been broken earlier today. If these efforts fail, we are ready to ensure safety of our population, 
and retaliate to every attack in kind.

I have already taken the measures, to ensure safety of every important member of the government and myself. Even if worst will come to past, we will still be able to continue the
fight, and rebuild our great country, after we mourn the dead.

We are ready, 
God bless our Country.

THIS CONCLUDES PRESIDENTIAL ADDRESS
"
            }, true);

            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendCivilDanger2Message_Elapsed(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "CIVIL DANGER WARNING - EVACUATE IMMEDIATELY - THIS IS NOT A TEST",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF THE GOVERNMENT. THIS IS NOT A TEST

National Civil Defense Service has issued an update to CIVIL DANGER WARNING issued for entire teritory of this country earlier.
THERE IS NO ATTACK COMMING. THIS IS A WARNING MESSAGE.

Due the developments in current crisis, and possibility of nuclear attack against this country, the government has decided to evacuate cities with population above 100 000 citzens.
If this message applies to you, take following steps
1. Prepare personal evacuation kit, for every member of your family. The kit should contain food, water and necessary medication for at least 72 hours. Do not pack unnecessary things such as jewelery, toys, clothes etc.
2. If you onw a personal vehicle, try to take your neighbours who don't own one with you.
3. Use only designated evacuation routes. Other routes should be kept open for emergency use.
4. If you don't have personal method of transport, go to your local public transport station. Public buses and trains will be in place to evacuate those in need.
5. Do not try to evacuate members of your family who are in schools or hospitals. Their evacuation will be ensured by National Civil Defense Service.
6. If you are not within immediate area of your home, go straight to the evacuation station. Emergency supplies will be assured by members of National Civil Defense Service.

Citzens of lesser populated areas continue their emergency precautions:
1. Prepare shelter in place - stock up in food, water and medical supplies for at least 14 days of stay. If possible, seek shelter in basements of sturdy buildings as far from windows as possible.
2. In case of evacuation order, prepare your personal evacuation kit for every member of your family. If you onw a personal vehlicle, plan evacuation with members of your community who don't own one.
3. Do not try to evacuate members of your family who are in schools or hospitals. Their safety will be ensured by National Civil Defense Service.
4. Do not use telephone lines. Telephone lines should be kept open for emergency use.
5. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

Regardless of your situation, remember these rules:
1. Always listen to directions provided by police, fire department, military and National Civil Defense Service officers on site.
2. Do not use telephone lines. Telephone lines should be kept open for emergency use.
3. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

The President will address the nation shortly after this message is issued.

THIS CONCLUDES CIVIL DANGER WARNING MESSAGE
"
            }, true);
            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendAllClearAirAttackMessage_Elapsed(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "ALL CLEAR - THIS IS NOT A TEST",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF GOVERNMENT. THIS IS NOT A TEST

National Civil Defense Service has issued a ALL CLEAR message for AIR ATTACK ALERT issued earlier.

Air Defense Forces had detected, that enemy planes had left airspace of this country

ALL CLEAR

It is safe to leave your shelter now. If you, or member of your family or community got hurt, National Civil Defense Service officers are in place to provide help.
Keep in mind, that CIVIL DANGER and EVACUATE IMMEDIATELY messages are still in effect. If your area had EVACUATE IMMEDIATELY message issued, you have to continue your evacuation.

1. Always listen to directions provided by police, fire department, military and National Civil Defense Service officers on site.
2. Do not use telephone lines. Telephone lines should be kept open for emergency use.
3. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

THIS CONCLUDES ALL CLEAR MESSAGE
"
            }, true);
            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendAirAttackWarning(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "AIR ATTACK ALERT - THIS IS NOT A TEST",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF AIR DEFENSE FORCES. THIS IS NOT A TEST

National Civil Defense Service has issued a AIR ATTACK ALERT message for entire teritory of this country.
AIR ATTACK ALERT MEANS, THAT THERE IS ATTACK IN PROGRESS.

Air Defense Forces had detected multiple enemy attack planes entering this country's airspace. Air Defense Forces will take steps to intercept those planes.

IT IS TOO LATE TO EVACUATE NOW. SEEK IMMEDIATE SHELTER.

Every citzen receiving this message is ordered to take immediate actions:
1. Seek immediate shelter. If possible, go to the basement of sturdy building.
2. If there is no basement in your current bulding, try to place as much space between you and windows and outside world.
3. If you are outside your home, don't go back there. Lead straight to public shelters, that are being set by National Civil Defense Service officers.
4. If you are on the road, pull of and try to seek immediate shelter in a building.
5. Do not try to evacuate members of your family who are in schools or hospitals. Their safety will be ensured by National Civil Defense Service.
6. Always listen to directions provided by police, fire department, military and National Civil Defense Service officers on site.
7. Do not use telephone lines. Telephone lines should be kept open for emergency use.
8. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

THIS CONCLUDES AIR ATTACK ALERT MESSAGE
"
            }, true);
            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendEvacuateImmediatlyMessage(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "EVACUATE IMMEDIATELY - THIS IS NOT A TEST",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF THE GOVERNMENT. THIS IS NOT A TEST

National Civil Defense Service has issued a EVACUATE IMMEDIATELY message for population of northern provinces of this country.
THERE IS NO ATTACK COMMING. THIS IS A WARNING MESSAGE.

Due the significant danger posed by hostile military force outside northern border of this country, the governemnt has decided to evacuate citzens from northern provinces. 

Every citzen receiving this message is ordered to take immediate actions:
1. Prepare personal evacuation kit, for every member of your family. The kit should contain food, water and necessary medication for at least 72 hours. Do not pack unnecessary things such as jewelery, toys, clothes etc.
2. If you onw a personal vehicle, try to take your neighbours who don't own one with you.
3. Use only designated evacuation routes. Other routes should be kept open for emergency use.
4. If you don't have personal method of transport, go to your local public transport station. Public buses and trains will be in place to evacuate those in need.
5. Do not try to evacuate members of your family who are in schools or hospitals. Their evacuation will be ensured by National Civil Defense Service.
6. If you are not within immediate area of your home, go straight to the evacuation station. Emergency supplies will be assured by members of National Civil Defense Service.
7. Always listen to directions provided by police, fire department, military and National Civil Defense Service officers on site.
8. Do not use telephone lines. Telephone lines should be kept open for emergency use.
9. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

THIS CONCLUDES EVACUATE IMMEDIATELY MESSAGE
"
            }, true);
            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendCivilDangerEmail(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "CIVIL DANGER WARNING - THIS IS NOT A TEST",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF THE GOVERNMENT. THIS IS NOT A TEST

National Civil Defense Service has issued a CIVIL DANGER WARNING for entire teritory of this country.
THERE IS NO ATTACK COMMING. THIS IS A WARNING MESSAGE.
Every citzen of this country is ordered to take following immediate precautions for possible military attack on this country:
1. Prepare shelter in place - stock up in food, water and medical supplies for at least 14 days of stay. If possible, seek shelter in basements of sturdy buildings as far from windows as possible.
2. In case of evacuation order, prepare your personal evacuation kit for every member of your family. If you onw a personal vehlicle, plan evacuation with members of your community who don't own one.
3. Do not try to evacuate members of your family who are in schools or hospitals. Their safety will be ensured by National Civil Defense Service.
4. Do not use telephone lines. Telephone lines should be kept open for emergency use.
5. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

THIS CONCLUDES CIVIL DANGER WARNING MESSAGE
"
            }, true) ;
            ((System.Timers.Timer)sender).Stop();
        }

        protected void SendFinalAllClearMessage_Elapsed(object sender, ElapsedEventArgs e)
        {
            _emailService.AddEmail(new Email()
            {
                Sender = "EMERGENCY ALERT SERVICE",
                Subject = "ALL CLEAR - THIS IS NOT A TEST",
                ReceivedDate = DateTime.Now,
                Content = @"
THIS MESSAGE IS BROADCASTED AT THE REQUEST OF GOVERNMENT. THIS IS NOT A TEST

National Civil Defense Service has issued a ALL CLEAR message for CIVIL DANGER message issued earlier.

This country is no longer under threat of hostile aggression.

ALL CLEAR

It is safe to leave your shelter now. If you, or member of your family or community got hurt, National Civil Defense Service officers are in place to provide help.

1. Always listen to directions provided by police, fire department, military and National Civil Defense Service officers on site.
2. Do not use telephone lines. Telephone lines should be kept open for emergency use.
3. Keep battery powered radio with you. Emergency Alert Service updates will be broadcasted through Radio, Television, Cellurar services and Internet.

THIS CONCLUDES ALL CLEAR MESSAGE
"
            }, true);
            ((System.Timers.Timer)sender).Stop();
        }

        public ScenarioBase(EmailCommand emailService)
        {
            _rng = new Random();
            _emailService = emailService;
            _triggers = new List<System.Timers.Timer>();
        }

        public abstract void ScenarioEndingLaunch();
        public abstract void ScenarioEndingShutdown();

        protected static void SimulateLaunch()
        {
            CancellAllEvents();
            Console.Clear();
            Thread.Sleep(2000);
            foreach (var c in "DEAD HAND SYSTEM FULLY OPERATIONAL")
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
            Thread.Sleep(2000);
            Console.WriteLine("");

            foreach (var c in "PROCEEDING TO AUTOMATICALLY LAUNCH BALLISTIC MISSLES TOWARDS DESIGNATED TARGETS")
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
            Thread.Sleep(5000);
            Console.WriteLine("");
            Console.Clear();
            foreach (var c in "GOOD BYE")
            {
                Console.Write(c);
                Thread.Sleep(1000);
            }
            Thread.Sleep(5000);
            Console.Clear();
        }

        protected void CancellAllEvents()
        {
            foreach (var item in _triggers)
            {
                item.Stop();
            }
        }
    }
}
