using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp {
    public class Reservations {
        private static List<string> allReservations;

        public static void ReservationMain() {
            allReservations = new List<string>();
            string fileName = "ReservationCodes+TicketOrders.txt";
            using (StreamReader streamreader = new StreamReader(fileName)) {
                string line;
                while (!string.IsNullOrWhiteSpace(line = streamreader.ReadLine())) {
                    allReservations.Add(line);
                }
            }
            Console.WriteLine("\nWhat would you like to do.\n[1] Show all reservations.\n[2] Remove a reservation.\n[0] Exit");
            int choice = Program.ChoiceInput(0, 2);
            while (choice != 0) {
                if (choice == 1) {
                    ShowReservations();
                } else {
                    RemoveReservations();
                }
                Console.WriteLine("\nWhat would you like to do.\n[1] Show all reservations.\n[2] Remove a reservation.\n[0] Exit");
                choice = Program.ChoiceInput(0, 2);
            }
        }

        public static void ShowReservations() 
            {
            if (allReservations.Count != 0) {
                for (int i = 0; i < allReservations.Count; i++) {
                    Console.WriteLine(allReservations[i]);
                }
            } else {
                Console.WriteLine("\nThere are no reservations");
            }
        }

        public static void RemoveReservations() {
            if (allReservations.Count != 0) {
                Console.WriteLine("\nWhat reservation would you like to remove");
                for (int i = 0; i < allReservations.Count; i++) {
                    Console.WriteLine($"{allReservations[i]}");
                }
                Console.WriteLine($"[0] Exit");
                int choice = Program.ChoiceInput(0, allReservations.Count) - 1;
                if (choice != -1) {
                    allReservations.RemoveAt(choice);
                }
                //streamwriter
                string fileName = "ReservationCodes+TicketOrders.txt";
                string s = "";
                for (int i = 0; i < allReservations.Count; i++) {
                    s += allReservations[i] + "\n";
                }
                File.WriteAllText(fileName, s);
            } else {
                Console.WriteLine("\nThere are no reservations");
            }
        }
    }
}
