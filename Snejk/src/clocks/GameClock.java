package clocks;

import actions.Collision;
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
                Collision.collidePickUp();
                if(Collision.collideSelf()) {
                    Snake.tails.clear();
                }
                if(Collision.collideWall()){
                    Snake.tails.clear();
                    Snake.head.setX(7);
                    Snake.head.setY(7) ;
                }
            }catch (InterruptedException e){
                e.printStackTrace();
            }
        }
    }
}
