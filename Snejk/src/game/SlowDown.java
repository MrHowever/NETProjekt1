package game;

import clocks.GameClock;

public class SlowDown extends PickUp
{
    public SlowDown()
    {
        super();
    }

    public void onEaten()
    {
        int currentSpeed = GameClock.sleepTime;
        new Thread(() -> {
                GameClock.sleepTime += 1000;
                try {
                    Thread.sleep(5000);
                }
                catch(Exception e)
                {

                }
                GameClock.sleepTime = currentSpeed;
        }).start();
    }
}
