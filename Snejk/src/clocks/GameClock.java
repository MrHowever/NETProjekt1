package clocks;

import game.*;

import java.util.ArrayList;
import java.util.Arrays;

public class GameClock extends Thread
{
    public static boolean running = true;
    public static int sleepTime = 500;
    public static ArrayList<Snake> snakes = new ArrayList<Snake>(Arrays.asList(new Snake(0)));
    public static Obstacle obstacle = new BlinkingObstacle();
    public static PickUp pickup = new SpeedUp();
    public static long deathAt = 0;
    public static long obstacleWait = 2000000000;

    public void run()
    {
        while(running)
        {
            try{
                sleep(sleepTime);

                for (Snake snake : snakes) {
                    snake.move();
                    snake.collidePickUp();

                    Snake.waitToMove = false;

                    for(Snake differentSnake : snakes) {
                        if (snake.collideSnake(differentSnake)) {
                            snake.tails.clear();
                            snake.head.setX(7);
                            snake.head.setY(7);
                            Scoreboard.scores.set(snake.ID,0);
                        }
                    }

                    if (snake.collideWall()) {
                        snake.tails.clear();
                        snake.head.setX(7);
                        snake.head.setY(7);
                        Scoreboard.scores.set(snake.ID,0);
                    }

                    if(obstacle.collision(snake.head.getX(),snake.head.getY()))
                    {
                        snake.tails.clear();
                        snake.head.setX(7);
                        snake.head.setY(7);
                        Scoreboard.scores.set(snake.ID,0);
                    }
                }

                pickup.action();

                if(obstacle.alive)
                    obstacle.action();

                createObstacle();
            }catch (InterruptedException e){
                e.printStackTrace();
            }
        }
    }

    static public void createObstacle()
    {
        if(!obstacle.alive && deathAt == 0) {
            deathAt = System.nanoTime();
        }

        if(!obstacle.alive && ((System.nanoTime() - deathAt) > obstacleWait)) {
           int randInt = (int) Math.round(Math.random()*2);

                switch(randInt) {
                    case 0:
                        obstacle = new MovingObstacle();
                        break;
                    case 1:
                        obstacle = new StaticObstacle();
                        break;
                    case 2:
                        obstacle = new BlinkingObstacle();
                        break;
            }

            deathAt = 0;
        }
    }

    static public void createPickup()
    {
        int randomVal = (int) (Math.round(Math.random()*30));

        if(randomVal <= 9)
            pickup = new StaticPickup();
        else if(randomVal <= 18)
            pickup = new Frog();
        else if(randomVal <= 27)
            pickup = new Berries();
        else if(randomVal <= 29)
            pickup = new SpeedUp();
        else if(randomVal <= 30)
            pickup = new SlowDown();
    }
}
