package clocks;

import actions.Collision;
import game.*;

import java.util.ArrayList;
import java.util.Arrays;

public class GameClock extends Thread
{
    public static boolean running = true;
    public static int sleepTime = 500;
    public static PickUp pickup = new Frog();
    public static ArrayList<Snake> snakes = new ArrayList<Snake>(Arrays.asList(new Snake(0)));
    public static Obstacle obstacle = new MovingObstacle();
    public static AI ai = new AI(snakes.get(0));

    public void run()
    {
        while(running)
        {
            try{
                sleep(sleepTime);

                for (Snake snake : snakes) {
                    snake.move();
                    Snake.waitToMove = false;
                    snake.collidePickUp();

                    for(Snake differentSnake : snakes) {
                        if (snake.collideSnake(differentSnake)) {
                            snake.tails.clear();
                            snake.head.setX(7);
                            snake.head.setY(7);
                            Scoreboard.scores.set(snake.ID,0);
                        }
                    }


                    if (snake.collideWall() || obstacle.collision(snake.head.getX(),snake.head.getY())) {
                        snake.tails.clear();
                        snake.head.setX(7);
                        snake.head.setY(7);
                        Scoreboard.scores.set(snake.ID,0);
                    }
                }

                pickup.action();
                obstacle.action();

                createObstacle();

                ai.initGrid(snakes.get(0));
                ai.AStar();

            }catch (InterruptedException e){
                e.printStackTrace();
            }
        }
    }

    static public void createObstacle()
    {
        double randomVal = Math.random();

        if(randomVal > 0.7 && obstacle.segments.size() == 0)
        {
            obstacle = new MovingObstacle();
        }
    }

    static public void createPickup()
    {
        int randomVal = (int) (Math.round(Math.random())*2);

        switch(randomVal)
        {
            case 0:
                pickup = new StaticPickup();
                break;
            case 1:
                pickup = new Frog();
                break;
            case 2:
                pickup = new Berries();
                break;
        }
    }
}
