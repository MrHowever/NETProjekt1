package clocks;

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
            }catch (InterruptedException e){
                e.printStackTrace();
            }
        }
    }
}
