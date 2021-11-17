using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Refactoring2ndCapture1 {
    class Program {
        static void Main(string[] args) {
            var plays = JsonSerializer.Deserialize<Dictionary<string, play>>(Properties.Resources.plays);
            var invoices = JsonSerializer.Deserialize<List<invoice>>(Properties.Resources.invoices);

            foreach(var invoice in invoices) {
                Console.WriteLine(statement(invoice, plays));
            }
        }

        static string statement(invoice invoice, Dictionary<string, play> plays) {
            int totalAmount = 0;
            int volumeCredits = 0;
            string result = $"Statement for {invoice.customer}\n";

            string format(decimal d) {
                return d.ToString("$#,#.00");
            }

            foreach(var perf in invoice.performances) {
                play play = plays[perf.playID];
                int thisAmount = 0;

                switch (play.type) {
                    case "tragedy":
                        thisAmount = 40000;
                        if (perf.audience > 30) {
                            thisAmount += 1000 * (perf.audience - 30);
                        }
                        break;
                    case "comedy":
                        thisAmount = 30000;
                        if (perf.audience > 20) {
                            thisAmount += 10000 + 500 * (perf.audience - 20);
                        }
                        thisAmount += 300 * perf.audience;
                        break;
                    default:
                        throw new ArgumentException($"unknown type:{play.type}");
                }

                //ボリューム特典のポイントを加算
                volumeCredits += Math.Max(perf.audience - 30, 0);
                //喜劇のときは5人につきさらにポイント加算
                if (play.type == "comedy") volumeCredits += (int)Math.Floor(perf.audience / 5m);
                //注文の内訳を出力
                result += $" {play.name}: {format(thisAmount / 100m)} ({perf.audience} seats)\n";
                totalAmount += thisAmount;
            }
            result += $"Amount owed is {format(totalAmount / 100m)}\n";
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }
    }
}
