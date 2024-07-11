using System;
using static System.Console;
namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.memberManager.addFirstMember(); //Adding one member firstname: Jane lastname: Smith Password:1234
            menu.movieManager.AddMovieSamples(); //Adding a few movies into the System.
            while (true) { menu.Login(); }
        }
    }
    class Menu
    {
        public MovieCollection movieManager = new MovieCollection();
        public MemberCollection memberManager = new MemberCollection();
        public void Login() //Very first Login page
        {
            WriteLine("=====================================================");
            WriteLine("COMMUNITY LIBRARY MOVIE AND DVD MANAGEMENT SYSTEM");
            WriteLine("=====================================================");
            WriteLine("");
            WriteLine("Main Menu");
            WriteLine("---------------------------");
            WriteLine("Select from the following: \n");
            WriteLine("1. Staff");
            WriteLine("2. Member");
            WriteLine("0. End the Program \n");
            Write("Enter your choice ==> ");
            bool valid = int.TryParse(ReadLine(), out int choice);
            while (!valid || choice < 0 || choice > 2)
            {
                Write("Please select 1, 2 or 0 >> ");
                valid = int.TryParse(ReadLine(), out choice);
            }
            switch (choice)
            {
                case 1:
                    Write("Enter your username ==> ");
                    string username = ReadLine();
                    Write("Enter you password ==> ");
                    string password = ReadLine();
                    if (verifyStaff(username, password))
                    {
                        int staffInput = -1;
                        while (staffInput != 0)
                        {
                            WriteLine("Staff Menu");
                            WriteLine("-----------------------------");
                            WriteLine("1. Add DVDs of a new movie to the sytem");
                            WriteLine("2. Add new DVDs of an existing movie to the system");
                            WriteLine("3. Remove a DVD from the system");
                            WriteLine("4. Register a member to the system");
                            WriteLine("5. Remove a registered member from the system");
                            WriteLine("6. Find a member's contact phone number given the member's name");
                            WriteLine("7. Find members who are currently renting a particular movie");
                            WriteLine("0. Exit to main menu \n");
                            Write("Enter your choice ==> ");
                            bool staffValid = int.TryParse(ReadLine(), out staffInput);
                            while (!staffValid || staffInput < 0 || staffInput > 7)
                            {
                                Write("Please input a number between 1 and 7 >> ");
                                staffValid = int.TryParse(ReadLine(), out staffInput);
                            }
                            StaffMenu(staffInput);
                        }
                    }
                    return;
                case 2:
                    Write("Enter your member first name >> ");
                    string memberfname = ReadLine();
                    Write("Enter your member last name >> ");
                    string memberlname = ReadLine();
                    Write("Enter your member password >> ");
                    string memberPassword = ReadLine();
                    if (memberManager.Verify(memberfname, memberlname, memberPassword))
                    {
                        int memberInput = -1;
                        while (memberInput != 0)
                        {
                            WriteLine("Member Menu");
                            WriteLine("-----------------------------");
                            WriteLine("1. Browse all the Movies");
                            WriteLine("2. Display all information about a movie, given the title of the movie");
                            WriteLine("3. Borrow a movie DVD");
                            WriteLine("4. Return a movie DVD");
                            WriteLine("5. List current borrowing movies");
                            WriteLine("6. Display top 3 movies rented by the members");
                            WriteLine("0. Exit to main menu \n");
                            Write("Enter your choice ==> ");
                            bool memberValid = int.TryParse(ReadLine(), out memberInput);
                            while (!memberValid || memberInput < 0 || memberInput > 6)
                            {
                                Write("Please input a number between 1 and 6 >> ");
                                memberValid = int.TryParse(ReadLine(), out memberInput);
                            }
                            MemberMenu(memberInput, memberfname, memberlname);
                        }
                    }
                    return;
                case 0:
                    Environment.Exit(0);
                    return;
            }
        }
        private bool verifyStaff(string username, string password)
        {
            if (username == "staff" && password == "today123") //checks to verify if the inputs are staff and today123
            {
                return true;
            }
            WriteLine("\n Username or password is incorrect \n");
            return false;
        }
        public void StaffMenu(int choice) //staff menu
        {

            switch (choice)
            {
                case 0:
                    return;
                case 1: //Add a new movie to the system
                    movieManager.Add();
                    return;
                case 2: //increase the quantity of a DVD for a movie
                    movieManager.increaseDVDQuantity();
                    return;
                case 3: //remove a movie
                    movieManager.Remove();
                    return;
                case 4: //Register a member
                    memberManager.Add();
                    return;
                case 5: //Unregister a member
                    memberManager.Remove();
                    return;
                case 6: //Find the contact number given the first and last name
                    memberManager.findContactNumber();
                    return;
                case 7: //Check all the members borrowing a particular movie
                    Write("Enter the name of the movie >> ");
                    string movieName = ReadLine();
                    Movie movie = movieManager.SearchMovie(movieName);
                    memberManager.membersBorrowing(movie);
                    return;
            }
        }
        public void MemberMenu(int choice, string fname, string lname) //member menu
        {
            switch (choice)
            {
                case 0:
                    return;
                case 1: //sort in alphabetical order
                    WriteLine("---------------------------- All Movies ------------------------------");
                    Movie[] movieList = movieManager.SortBy("name");
                    foreach (Movie movie in movieList)
                    {
                        WriteLine(movie);
                    }
                    WriteLine("----------------------------------------------------------------------");
                    return;
                case 2: //Display all the movies given the title
                    Write("Enter the name of the movie >> ");
                    string searchMovieName = ReadLine();
                    Movie movieName = movieManager.SearchMovie(searchMovieName);
                    if (movieName != null)
                    {
                        WriteLine("\n {0} \n", movieName.ToString());
                        return;
                    }
                    WriteLine("\n This movie doesn't exist \n");
                    return;
                case 3: //Borrow a movie DVD"=
                    Write("Enter the name of the movie you wish to borrow >> ");
                    string borrowMovieName = ReadLine();
                    Movie borrowMovie = movieManager.SearchMovie(borrowMovieName);
                    if (borrowMovie == null)
                    {
                        WriteLine("\n Movie does not exist \n");
                        return;
                    }
                    memberManager.Borrow(fname, lname, borrowMovie);
                    return;
                case 4: //return a movie
                    Write("Enter the name of the movie you wish to return >> ");
                    string returnMovieName = ReadLine();
                    Movie returnMovie = movieManager.SearchMovie(returnMovieName);
                    if (returnMovie == null)
                    {
                        WriteLine("\n Movie does not exist \n");
                        return;
                    }
                    memberManager.Return(fname, lname, returnMovie);
                    return;
                case 5: //see movies currently borrowed by you
                    memberManager.seeCurrentMovies(fname, lname);
                    return;
                case 6: //see top 3 most borrowed movies
                    WriteLine("---------------------------- Top 3 Most Borrowed Movies ------------------------------");
                    Movie[] mostBorrowedMovieList = movieManager.SortBy("name");
                    for (int i = 0; i <= 2; i++)
                    {
                        WriteLine($"Movie: {mostBorrowedMovieList[i].Title} || Number of Borrows: {mostBorrowedMovieList[i].borrowedCount}");
                    }
                    WriteLine("----------------------------------------------------------------------");
                    return;
            }
        }
    }


    class Member
    {
        private int id;
        private string firstName;
        private string lastName;
        private int password;
        private int phoneNumber;
        private Movie[] borrowing = new Movie[5];
        public string FirstName
        {
            get { return firstName; }
        }
        public string LastName
        {
            get { return lastName; }
        }
        public int Password
        {
            get { return password; }
        }
        public int PhoneNumber
        {
            get { return phoneNumber; }
        }
        public Member(int id, string fname, string lname, int phone, int password)
        {
            this.id = id;
            firstName = fname;
            lastName = lname;
            phoneNumber = phone;
            this.password = password;
        }
        public override string ToString()
        {
            return $"ID: {id}, First Name: {firstName}, Last Name: {lastName}, Phone: {phoneNumber}";
        }
        public void BorrowMovie(Movie movie)
        {
            int i = 0;
            while (i < borrowing.Length)
            {
                if (borrowing[i] == null)
                {
                    borrowing[i] = movie; //insert the movie into the array
                    WriteLine("\n You have borrowed {0} \n", movie.Title);
                    movie.borrowedCount++; //increase the movie's borrowing count by 1
                    movie.available--; //decrease the movie's available quantity by 1
                    return;
                }
                i++;
            }
            WriteLine("\n You have reached the borrowing capacity of DVDs \n");
            return;
        }
        public void ReturnMovie(Movie movie)
        {
            int i = 0;
            while (i < borrowing.Length)
            {
                if (borrowing[i] != null)
                {
                    if (borrowing[i] == movie)
                    {
                        borrowing[i] = null; //remove the movie from the array
                        movie.available++; //increase the movie's available quantity by 1
                        WriteLine("\n {0} has been returned \n", movie.Title);
                        return;
                    }
                }
                i++;
            }
            WriteLine("\n {0} is not borrowed by you \n", movie.Title);
            return;
        }
        public void showCurrentMovies()
        {
            int i = 0;
            WriteLine("-------------- Current Movies rented by you -----------------");
            while (i < borrowing.Length)
            {
                if (borrowing[i] != null)
                {
                    WriteLine(borrowing[i].ToString()); //Write all movies borrowed by you
                }
                i++;
            }
            WriteLine("-------------------------------------------------------------");
        }
        public bool isBorrowing(Movie movie)
        {
            for (int i = 0; i < borrowing.Length; i++)
            {
                if (borrowing[i] != null) //check if borrowing is null
                {
                    if (movie == borrowing[i]) //if not null then, return true
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    class MemberCollection
    {
        private Member[] collection;
        private int memberCount;
        public MemberCollection()
        {
            collection = new Member[1000]; //Maximum member capacity of 1000
        }
        public void Add()
        {
            if (memberCount == 1000) //A maximum capacity of 1000
            {
                Console.WriteLine("Insufficient storage to add new members. Please delete Members");
                return;
            }
            Console.Write("Please input First Name >> ");
            string fname = Console.ReadLine();
            Console.Write("Please input Last Name >> ");
            string lname = Console.ReadLine();
            Console.Write("Please input password >> ");
            if (SearchKey(fname, lname) != -1)
            {
                WriteLine("\n This member exists \n"); //Member validation, cannot create a member with the same first and last name
                return;
            }
            string unvalidatedPassword = Console.ReadLine();
            while (InputValidation("password", unvalidatedPassword) != true)
            {
                Console.Write("This is not a valid password. It must be a 4-digit PIN >> ");
                unvalidatedPassword = Console.ReadLine();
            }
            int password = int.Parse(unvalidatedPassword);
            Console.Write("Please input Phone Number >> ");
            string phone = Console.ReadLine();
            while (InputValidation("integer", phone) != true) //Input validation for an integer
            {
                Console.Write("This is not a valid Phone Number. Please use an integer >> ");
                phone = Console.ReadLine();
            }
            int pnumber = int.Parse(phone);
            int i = 0;
            while (collection[i] != null)
            {
                i++;
            }
            Member newMember = new Member(i + 1, fname, lname, pnumber, password); //create new instance of member
            collection[i] = newMember;
            memberCount++;  //Add member count
            WriteLine("\n Member Added \n");
        }
        public void Remove() //remove by first and last name
        {
            Console.Write("Please write the First Name of the person to unregister >> ");
            string name = Console.ReadLine();
            Console.Write("Please write the Last Name of the person to unregister >> ");
            string lname = Console.ReadLine();
            int index = SearchKey(name, lname);
            if (index == -1)
            {
                Console.WriteLine("\n This member is not found \n");
                return;
            }
            collection[index] = null;
            memberCount--; //decrease membercount by 1
        }
        public void Borrow(string fname, string lname, Movie movie) //Borrow a movie
        {
            int index = SearchKey(fname, lname);
            if (movie.available <= 0) //check if the movie is in stock
            {
                WriteLine($"\n {movie.Title} is not in stock \n");
                return;
            }
            collection[index].BorrowMovie(movie);
        }
        public void Return(string fname, string lname, Movie movie)
        {
            int index = SearchKey(fname, lname);
            collection[index].ReturnMovie(movie);
        }
        private int SearchKey(string fname, string lname) //searches the index of the member given the first name and last name
        {
            int i = 0;
            while (i < collection.Length)
            {
                if (collection[i] != null)
                {
                    if (collection[i].FirstName == fname && collection[i].LastName == lname)
                    {
                        WriteLine(i);
                        return i;
                    }
                }
                i++;
            }
            return -1;
        }
        public bool Verify(string name, string lastname, string pword) //verifies the login password and username
        {
            int.TryParse(pword, out int password);
            int i = 0;
            while (i < collection.Length)
            {
                if (collection[i] != null)
                {
                    if (collection[i].FirstName == name && collection[i].LastName == lastname && collection[i].Password == password)
                    {
                        return true;
                    }
                }
                i++;
            }
            WriteLine("\n Name or password is incorrect \n");
            return false;
        }
        public void seeAllMembers()
        {
            int i = 0;
            while (i < collection.Length)
            {
                if (collection[i] != null)
                {
                    WriteLine(collection[i]);
                }
                i++;
            }
        }
        private bool InputValidation(string type, string input)
        {
            switch (type)
            {
                case ("integer"):
                    bool isInt = int.TryParse(input, out int value);
                    if (isInt == true)
                    {
                        return isInt;
                    }
                    return false;
                case ("password"):
                    bool isPassword = int.TryParse(input, out int password);
                    if (isPassword && password.ToString().Length == 4)
                    {
                        return isPassword;
                    }
                    return false;
            }
            return false;
        }
        public void findContactNumber() //Find the contact number of a member given their first and last name
        {
            WriteLine("What is the first name of the member >>");
            string fname = ReadLine();
            WriteLine("What is the last name of the member >>");
            string lname = ReadLine();
            int index = SearchKey(fname, lname);
            if (index != -1) //if SearchKey method returns -1 then the user does not exist
            {
                WriteLine("\n The phone number of {0} {1} is {2} \n", fname, lname, collection[index].PhoneNumber);
                return;
            }
            WriteLine("\n The name {0} {1} does not exist in the system \n");
        }
        public void seeCurrentMovies(string fname, string lname) //shows all movies borrowed by you
        {
            int index = SearchKey(fname, lname);
            collection[index].showCurrentMovies();
        }
        public void membersBorrowing(Movie movie) //shows all movie a member is borrowing
        {
            WriteLine("----------------------- List of Names borrowing {0} ----------------------------", movie.Title);
            foreach (Member person in collection)
            {
                if (person != null)
                {
                    if (person.isBorrowing(movie))
                    {
                        WriteLine(person);
                    }
                }
            }
            WriteLine("--------------------------------------------------------------------------------");
        }
        public void addFirstMember() //just a code to add the first member
        {
            collection[0] = new Member(1, "Jane", "Smith", 0412933234, 1234);
        }
    }
    class Movie
    {
        private string title;
        private string genre;
        private string classification;
        public int borrowedCount { get; set; }
        public int quantity { get; set; }
        public int available { get; set; }
        public string Title
        {
            get { return title; }
        }
        public Movie(string title, string genre, string classification)
        {
            this.title = title;
            this.genre = genre;
            this.classification = classification;
            quantity = 1;
            available = 1;
            borrowedCount = 0;
        }
        public override string ToString()
        {
            return $"Movie: {title} || Genre: {genre} || Classification: {classification} || quantity: {quantity} || in stock: {available}";
        }
    }
    class MovieCollection
    {
        private int capacity;
        private string[] keys;
        private Movie[] values;
        private int count;
        public MovieCollection()
        {
            count = 0;
            capacity = 1433; //Since the cap is 1000, I have chosen to increase the capacity by a small factor to avoid collisions when hashing.
            keys = new string[capacity];
            values = new Movie[capacity];
        }

        public void Add()
        {
            Console.Write("What is the name of the movie >> ");
            string name = Console.ReadLine();
            Console.WriteLine("What is the Genre?");
            Console.Write("Please input (Drama, Adventure, Family, Action, Sci-Fi, Comedy, Animated, Thriller, or Other) >> ");
            string genre = Console.ReadLine();
            bool isValidGenre = inputValidation("genre", genre);
            while (isValidGenre != true) //input validation for genre
            {
                Console.WriteLine("What is the Genre?");
                Console.Write("Please input EXACTLY (Drama, Adventure, Family, Action, Sci-Fi, Comedy, Animated, Thriller, or Other) >> ");
                genre = Console.ReadLine();
                isValidGenre = inputValidation("genre", genre);
            }
            Console.WriteLine("What is the classification of the Movie?");
            Console.Write("Please input the ACRONYMS General (G), Parental Guidance (PG), Mature (M15+) >> ");
            string classification = Console.ReadLine();
            bool isValidClassification = inputValidation("classification", classification);
            while (isValidClassification != true)  //Input validation for Classification
            {
                Console.WriteLine("What is the classification of the Movie?");
                Console.Write("Please input the ACRONYMS 'G' for General, 'PG' for Parental Guidance, 'M15+' for Mature >> ");
                classification = Console.ReadLine();
                isValidClassification = inputValidation("classification", classification);
            }
            Movie newMovie = new Movie(name, genre, classification);
            int index = Hash(name);
            while (keys[index] != null)
            {
                if (keys[index] == name) //check if movie exists since all names are unique
                {
                    Console.WriteLine("This movie already exists");
                    return;
                }
                index = (index + 1) % capacity; //linear probing
            }
            keys[index] = name;
            values[index] = newMovie;
            count++; //increase the number of unique movie names
        }
        public void Remove()
        {
            Console.Write("What is the name of the movie you would like to remove >> ");
            string name = Console.ReadLine();
            int index = SearchIndex(name); //searching if the movie exists
            if (index != -1)
            {
                keys[index] = null; //if it exists, delete the movie by making the keys and values null
                values[index] = null;
                count--;
                Console.WriteLine("Movie '{0}' removed from list", name);
            }
            else
            {
                Console.WriteLine("Movie '{0}' does not exist in the list"); //otherwise, print it doesn't exist
            }
        }
        public void increaseDVDQuantity() //increases the quantity of a DVD
        {
            Write("Enter the name of the movie >> ");
            string movieName = ReadLine();
            int index = SearchIndex(movieName);
            if (index == -1) //check if the movie exists
            {
                WriteLine("\n No such movie exists \n");
                return;
            }
            Movie movie = values[index]; //The movie exists and movie is the variable for the movie object
            Write("The current quantity of the movie is {0}, Enter the amount you would like to increase it by >> ", movie.quantity);
            bool isInt = int.TryParse(ReadLine(), out int quantityIncrease); //input validation for an integer to remove bugs
            while (!isInt)
            {
                Write("That is not a valid input. Please enter an integer >> ");
                isInt = int.TryParse(ReadLine(), out quantityIncrease);
            }
            movie.quantity = movie.quantity + quantityIncrease; //increase the quantity of the product by that quantity increase
            WriteLine("\n {0} new DVDs have been added to {1}. There is now {2} DVDs \n", quantityIncrease, movie.Title, movie.quantity);
        }
        private int SearchIndex(string key) //searches the index of the movie given the title name
        {
            int index = Hash(key);
            int beginning = index;
            while (keys[index] != null)
            {
                if (keys[index] == key)
                {
                    return index;
                }
                index = (index + 1) % capacity;
                if (index == beginning)
                {
                    return -1;
                }
            }
            return -1;
        }
        public Movie[] SortBy(string input) //sort by TITLE or BORROWEDCOUNT
        {
            Movie[] movieList = new Movie[count];
            int a = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] != null)
                {
                    movieList[a] = values[i];
                    a++;
                }
            }
            switch (input)
            {
                case ("Title"):
                    QuickSortByName(movieList, 0, movieList.Length - 1);
                    return movieList;
                case ("borrowedCount"):
                    QuickSortBorrowedCount(movieList, 0, movieList.Length - 1);
                    return movieList;
            }
            return movieList;
        }
        private Movie[] QuickSortByName(Movie[] array, int firstIndex, int lastIndex) //quick sort for all movies in alphabetical order
        {
            int i = firstIndex;
            int j = lastIndex;
            Movie pivot = array[firstIndex];

            while (i <= j)
            {
                while (array[i].Title.CompareTo(pivot.Title) < 0)
                {
                    i++;
                }
                while (array[j].Title.CompareTo(pivot.Title) > 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    Movie temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
            if (firstIndex < j)
            {
                QuickSortByName(array, firstIndex, j);
            }
            if (i < lastIndex)
            {
                QuickSortByName(array, i, lastIndex);
            }
            return array;
        }
        private Movie[] QuickSortBorrowedCount(Movie[] array, int firstIndex, int lastIndex) //quicksort for all movies by borrowedcount
        {
            int i = firstIndex;
            int j = lastIndex;
            Movie pivot = array[firstIndex];

            while (i <= j)
            {
                while (array[i].borrowedCount > pivot.borrowedCount)
                {
                    i++;
                }
                while (array[j].borrowedCount < pivot.borrowedCount)
                {
                    j--;
                }
                if (i <= j)
                {
                    Movie temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
            if (firstIndex < j)
            {
                QuickSortBorrowedCount(array, firstIndex, j);
            }
            if (i < lastIndex)
            {
                QuickSortBorrowedCount(array, i, lastIndex);
            }
            return array;
        }
        public Movie SearchMovie(string key) //Search the movie but returns the movie object instead of the index
        {
            int index = SearchIndex(key);
            if (index != -1)
            {
                return values[index];
            }
            return null;
        }
        private bool inputValidation(string type, string input) //validate inputs for genre and classification
        {
            switch (type)
            {
                case ("genre"):
                    string[] genre = { "Drama", "Adventure", "Family", "Action", "Sci-Fi", "Comedy", "Animated", "Thriller", "Other" };
                    for (int i = 0; i < genre.Length; i++)
                    {
                        if (input == genre[i])
                        {
                            return true;
                        }
                    }
                    return false;
                case ("classification"):
                    string[] classification = { "G", "PG", "M15+" };
                    for (int i = 0; i < classification.Length; i++)
                    {
                        if (input == classification[i])
                        {
                            return true;
                        }
                    }
                    return false;
            }
            return false;
        }
        private int Hash(string key)
        {
            int h = 0;
            foreach (char c in key)
            {
                h = (h * 31 + (int)c) % capacity; //formula for hashing the string. This formula is shown to avoid collisions.
            }
            return h;
        }
        public void AddMovieSamples() //Adding some movie samples for testing.
        {
            string[] movieNames = new string[] { "Jurassic Park", "Godfather", "Peppa Pig Movie", "Hero 5", "Hero 6" };
            string[] genres = new string[] { "Action", "Action", "Family", "Animated", "Comedy" };
            string[] classification = new string[] { "PG", "M15+", "G", "PG", "PG" };
            for (int i = 0; i < movieNames.Length; i++)
            {
                Movie newMovie = new Movie(movieNames[i], genres[i], classification[i]);
                int index = Hash(movieNames[i]);
                keys[index] = movieNames[i];
                values[index] = newMovie;
                count++;
            }
        }
    }
}
