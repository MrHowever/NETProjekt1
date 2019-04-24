import java.awt.EventQueue;

public class App {
    public static void main(String[] args) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                Window frame = new Window();
                frame.setVisible(true);
            }
        });
    }
}
