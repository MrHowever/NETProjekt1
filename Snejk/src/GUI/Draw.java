package GUI;
import clocks.GameClock;
import game.Scoreboard;
import game.Snake;

import javax.swing.*;
import java.awt.*;

public class Draw extends JLabel
{
    Point p;

    protected void paintComponent(Graphics g)
    {
        super.paintComponent(g);
        Graphics2D g2d = (Graphics2D) g;
        g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_OFF);

        //Draw Background
        g.setColor(Color.PINK);
        g.fillRect(0, 0, Gui.width, Gui.height);

        for(Snake snake : GameClock.snakes) {
            //Draw Snake Tails
            g.setColor(new Color(179, 204, 111));
            for (int i = 0; i < snake.tails.size(); i++) {
                p = Snake.ptc(snake.tails.get(i).getX(), snake.tails.get(i).getY());
                g.fillRect(p.x, p.y, 32, 32);
            }

            //Draw Snake Head
            g.setColor((new Color(156, 204, 0)));
            p = Snake.ptc(snake.head.getX(), snake.head.getY());
            g.fillRect(p.x, p.y, 32, 32);
        }

        //Draw obstacle
        g.setColor(new Color(0, 0, 100));
        for (int i = 0; i < GameClock.obstacle.segments.size(); i++) {
            p = Snake.ptc(GameClock.obstacle.segments.get(i).getX(), GameClock.obstacle.segments.get(i).getY());
            g.fillRect(p.x, p.y, 32, 32);
        }

        //Draw PickUp
        g.setColor(new Color(204, 71, 100));
        p = Snake.ptc(GameClock.pickup.getX(), GameClock.pickup.getY());
        g.fillRect(p.x, p.y, 32,32);

        //Draw Grid
        g.setColor(Color.DARK_GRAY);
        for(int i = 0; i < 16; i++)
        {
            for(int j = 0; j < 16; j++)
            {
                g.drawRect(i * 32 + Gui.xoff, j * 32 + Gui.yoff, 32, 32 );
            }
        }

        //Draw Border
        g.setColor(Color.DARK_GRAY);
        g.drawRect(Gui.xoff, Gui.yoff, 512, 512);

        //Draw Score
        g.setFont((new Font("Serif", Font.BOLD, 20)));
        g.drawString("Score 1: "+ Scoreboard.scores.get(0), 6,25);

        for(int i = 1; i < Scoreboard.scores.size(); i++)
            g.drawString("Score " + (i+1) + ": "+Scoreboard.scores.get(i),6,25 + 25*i);

        g.drawString("Best: "+Scoreboard.bestscore, 660, 25);
        repaint();
    }
}
