/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class Database {
    private static final String URL  = "jdbc:postgresql://localhost:5432/UniversityResources";
    private static final String USER = "postgres";
    private static final String PASSWORD = "password";
    private static Connection connection = null;
    
    Database() {
    }
    
    public static Connection getConnection()  {
        return connection;
    }
    
    public static void createConnection() {
        try {
         connection = DriverManager.getConnection(URL, USER, PASSWORD);
            connection.setAutoCommit(false);
        } catch (SQLException e) {
            System.err.println("Cannot connect to DB: " + e);
        }
    }
    
    public static void closeConnection() throws SQLException {
        Database.connection.close();
    }
}
