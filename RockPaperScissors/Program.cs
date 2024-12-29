Random random = new Random();

bool PlayAgain = true;
string computer;
string player;
string response = "";
while (PlayAgain == true)
{
    player = "";
    computer = "";
    while (player != "ROCK" && player != "PAPER" && player != "SCISSORS")
    {
        Console.Write("Rock Paper Or Scissors? ");
        player = Console.ReadLine();
        player = player.ToUpper();

    }

    switch (random.Next(1, 4))
    {


        case 1:
            computer = "ROCK";
            break;


        case 2:
            computer = "PAPER";
            break;



        case 3:
            computer = "SCISSORS";
            break;
    }
    Console.WriteLine("Player: " + player);
    Console.WriteLine("Computer: " + computer);
    switch (player)
    {
        case "ROCK":
            if (computer == "ROCK")
            {
                Console.WriteLine("Draw!");
            }

            else if (computer == "PAPER")
            {
                Console.WriteLine("You Lose!");
            }

            else
            {
                Console.WriteLine("You Win!");
            }


            break;

        case "PAPER":
            {
                if (computer == "ROCK")
                {
                    Console.WriteLine("You Win!");
                }

                else if (computer == "PAPER")
                {
                    Console.WriteLine("Draw!");
                }

                else
                {
                    Console.WriteLine("You Lose!");
                }
            }

            break;

        case "SCISSORS":
            {
                if (computer == "ROCK")
                {
                    Console.WriteLine("You Lose!");
                }
                else if (computer == "PAPER")
                {
                    Console.WriteLine("You Win!");
                }
                else
                {
                    Console.WriteLine("Draw!");
                }
                break;
            }
    }

    Console.WriteLine("Would you like to play again? Y/N");
    response = Console.ReadLine();
    response = response.ToUpper();
    if (response == "Y")
    {
        PlayAgain = true;
    }
    else
    {
        PlayAgain = false;
    }

    if (PlayAgain == false)
    {
        Console.WriteLine("Thanks For playing");
    }
}
