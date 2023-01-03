using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;
using System.Collections.Generic;

namespace DeadHand.Scenarios.Implementations
{
    internal class FalseWarningScenario : ScenarioBase
    {        
        public FalseWarningScenario(EmailCommand emailService, CheckRadioCommand radioService, WeatherServiceCommand weatherServiceCommand) : base(emailService, radioService, weatherServiceCommand)
        {
            radioService.SetCommandData("National Radio Channel 4");
            weatherServiceCommand.SetCommandData("Delivers latest update from Naval Weather Service", "\nTemperature: 25°C/77°F\nWind: 10KPH/6.21MPH\nCloudy\nHumidity: 10%");
            _emails = new Dictionary<int, Email>
            {
                { 60 * 1000 ,                 
                new Email()
                {
                    ReceivedDate = DateTime.Now,
                    Subject = "Order no. 583",
                    Sender = "command@stratcom.com",
                    Content = @"To asset "+Environment.UserName+@", 
Since peace talks in Geneva have been cancelled, and hostile armed force has issued red alert to their strategic weapons division, Strategic Command has issued STANDBY alert.

1. "+Environment.UserName+" has been authorized to enter following Dead Hand activation code: 2DCJ-CA83-8A9H-A9HD" +
"\n2. 10 minutes after activation Dead Hand will automatically launch retaliationary nuclear strike against enemy cities" +
"\n3. In final 4 minutes before activation "+Environment.UserName+" will get the chance to enter following delay code: F7SA-USA7-JA98-CDSA. Entering that code will delay retaliation for 7 minutes"+
"\n4. To help "+Environment.UserName+" make decision whether enter the code and wait another 10 minutes, or allow attack to commence, following checks may be performed:"+
"\na) Check whether National Radio program 4 is still broadcasting"+
"\nb) Check whether Naval Wether Service stil issues weather updates"+
"\nIn addition, all National Civil Defense Service broadcasts will be redirected to "+Environment.UserName+" email inbox in text form"+
"\n5. "+Environment.UserName+" is responsible for operation of computer controling Dead Hand system"+
"\n6. Dead Hand may be shut down only by entering shutdown code, that will be issued in ALL CLEAR message from Strategic Command"
                } },
                { 2 * 60 * 1000 ,
                    new Email()
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
",
                ProgrammingType = ProgrammingType.specialBulletin

            }
                },
                {
                    7 * 60 * 1000, new Email()
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
",
                ProgrammingType = ProgrammingType.specialBulletin
            }
                },
                {
                    11 * 60 * 1000, new Email()
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
",
                ProgrammingType = ProgrammingType.specialBulletin
            }
                },
                {
                    15 * 60 * 1000, new Email()
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
",
                ProgrammingType = ProgrammingType.specialBulletin
            }
                },
                {
                    16 * 60 * 1000, new Email()
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
",
                ProgrammingType = ProgrammingType.specialBulletin
            }

                },
                {
                    20 * 60 * 1000,new Email()
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
",
                ProgrammingType = ProgrammingType.presidentialAddress
            }
                },
                {
                    24 * 60 * 1000, new Email()
            {
                Sender = "command@stratcom.com",
                Subject = "Order no. 584",
                ReceivedDate = DateTime.Now,
                Content = $"Asset {Environment.UserName}," +
                @"The hostile government has issued stand down order to its military forces, and expressed will to resume peace talks.
Our intelligence has noted, that hostile forces are indeed standing down and returning to their bases." +
$"With that in mind, Startegic Command authorizes {Environment.UserName} to enter following Dead Hand shutdown code IMMEDIATELY\n" +
@"FA7S-I82B-HEY4-HWEF"

            }
                },
                {
                   23 * 60 * 1000, new Email()
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
",
                ProgrammingType = ProgrammingType.specialBulletin
            }
                }
            };
        }

        public override string ScenarioName => "False warning";

        public override string EndingLaunchText => @"
EPILOGUE: 
Asset responsible for Dead Hand system, had mistaken military exercise of 'hostile force' as a ligitimate threat, and allowed automatic retaliation system to perform
nuclear attack.

'Hostile force' seeing hundrets of ballistic missles coming their way, launched their own nuclear attack. Other nations seeing that laucnched their own arsennals, mutually destroying
each other in this process.
" + $"{Environment.UserName} has starved to death, after supplies ended in his bunker\n\n" +
$"Scenario name: False warning\n" +
$"This scenario has one more ending\n" +
$"" +
$"Thanks for playing Dead Hand, developed by kszaku in October of 2020\n";

        public override string EndingShutdownText => @"
EPILOGUE: 
(newspaper article printed following day)
After hours of talks behind closed doors, during press conference in Geneva representatives of both countries anounced, that they succesfully signed the peace treaty.
The treaty is promising the end of tensions between two nuclear powers, that had their tipping point yesterday, as reportedly both countries issued high alerts to their military
forces.
Both leaders expressed their satisfaction of fact, that total nuclear disarnment has found its place in the threaty.

UN General Secretary expressed gratitude saying 'May this be last time, that world comes this close to nuclear anihilation. Nuclear weapons are deeply
immoral, and pose existential threat to all of humanity. We must seek complete destruction of such tools'.

However, no leader wanted to comment, whether infamous 'Dead hand' system actually exists.
" +
$"Scenario name: False warning\n" +
$"This scenario has one more ending\n" +
$"" +
$"Thanks for playing Dead Hand, developed by kszaku in October of 2020\n";

    }
}
