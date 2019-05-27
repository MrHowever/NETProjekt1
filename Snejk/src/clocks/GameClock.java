package clocks;

import game.Snake;

public class GameClock extends Thread
{
    public static boolean running = true;
    public static int sleepTime = 200;

    public void run()
    {
        while(running)
        {
            try{
                sleep(sleepTime);
                Snake.move();
                Snake.waitToMove = false;
            }catch (InterruptedException e){
                e.printStackTrace();
            }
        }
    }
}
