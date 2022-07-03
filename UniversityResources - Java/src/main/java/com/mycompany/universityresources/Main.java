/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources;

import javax.swing.*;
import java.io.IOException;
import java.sql.SQLException;

/**
 * Main Class of the application
 */
public class Main {   
    public static void main(String[] args) throws IOException, SQLException {
        Database.createConnection();
        JFrame mainFrame  = new MainMenu();
        mainFrame.setVisible(true);
    }   
}
