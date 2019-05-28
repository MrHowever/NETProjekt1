import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Random;


public class Window extends JFrame{
    private JButton button1;
    private JPanel panel1;

    public Window() {
        super("Window");
        panel1.setLayout(null);
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        button1.setBounds(screenSize.width/2,screenSize.height/2,100,60);
        button1.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                Random generator = new Random();
                Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
                int x1 = generator.nextInt(screenSize.width-100);
                int y1 = generator.nextInt(screenSize.height-60);

                Rectangle startingPosition = button1.getBounds();
                int xOffset = (x1 - startingPosition.x)/100;
                int yOffset = (y1 - startingPosition.y)/100;

                new Thread(() -> {
                    for(int i = 0; i < 100; i++) {
                        Rectangle currentPosition = button1.getBounds();
                        button1.setBounds(currentPosition.x + xOffset, currentPosition.y + yOffset, 100, 60);
                        revalidate();
                        repaint();

                        try {
                            Thread.sleep(10);
                        }
                        catch(InterruptedException exception)
                        {

                        }
                    }
                   /* for(int i = 0; i < 100; i++) {
                        button1.setBounds(currentPosition.x+(xOffset*(i/100)),
                                currentPosition.y+(yOffset*(i/100)),100,60);

                        repaint();
                    }*/
                }).start();

                //button1.setBounds(x1,y1,100,60);
            }
        });

        panel1.add(button1);
        setContentPane(panel1);
        setUndecorated(true);
        setBackground(new Color(0,0,0,0));
        setSize(screenSize.width,screenSize.height);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

}
