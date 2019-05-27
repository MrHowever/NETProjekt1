package GUI;
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

        //Draw Snake Tails
        g.setColor(new Color(179,204, 111));
        for(int i = 0; i < Snake.tails.size(); i++){
            p = Snake.ptc(Snake.tails.get(i).getX(),Snake.tails.get(i).getY());
            g.fillRect(p.x, p.y, 32, 32 );
        }

        //Draw Snake Head
        g.setColor((new Color(156, 204, 0)));
        p = Snake.ptc(Snake.head.getX(),Snake.head.getY());
        g.fillRect(p.x, p.y,32,32 );

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

        repaint();
    }
}
