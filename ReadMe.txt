Using Log

			dynamic liss = new List<string>() { "deni", "deni1", "deni2", "deni3", "deni4", "deni5", "deni6", "deni7", "deni8" };
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1,"dd1");
            dic.Add(2, "dd2");
            Category cat = LogFactory.GetCategory("AbstractMessage");
            cat.AddDestination(TypeDestination.TypeConsole);
            cat.EnterMethod();
            cat.SendToDictionary<int, string>(dic);
            cat.ExitMethod();
            Category cat1 = LogFactory.GetCategory("Message");
            cat1.AddDestination(TypeDestination.TypeFile);
            cat1.EnterMethod();
            cat1.cat.SendString("Log");
            cat1.ExitMethod();

            Console.ReadKey();