package game;

import clocks.GameClock;

public class SpeedUp extends PickUp {

    public SpeedUp()
    {
        super();
    }

    public void onEaten()
    {
        new Thread(() -> {
            GameClock.sleepTime = 50;
            try {
                Thread.sleep(3000);
            }
            catch(Exception e)
            {

            }
            GameClock.sleepTime = 300;
        }).start();
    }
}
