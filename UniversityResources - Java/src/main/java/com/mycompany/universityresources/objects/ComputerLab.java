/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.objects;

import java.sql.*;

import static com.mycompany.universityresources.objects.RoomType.ROOM_COMPUTER_LAB;
import com.mycompany.universityresources.Database;
/**
 * @author alexc
 */
public class ComputerLab extends Room {
    private final String OS;
    
    public ComputerLab(String name, int capacity, int scheduleID, String os) throws SQLException {
        super(name, capacity, scheduleID);
        this.type = ROOM_COMPUTER_LAB;
        Connection con = Database.getConnection();
        this.OS = os;
        try (Statement stmt = con.createStatement();
             ResultSet rs = stmt.executeQuery(
               "SELECT nextval('roomid')")) {
            this.id =  rs.next() ? rs.getInt(1) : null;
            try (PreparedStatement pstmt = con.prepareStatement(
                    "insert into public.rooms (id, capacity, scheduleID, name,type,os) values (?,?,?,?,?,?)")) {
                pstmt.setInt(1, this.id);
                pstmt.setInt(2, capacity);
                pstmt.setInt(3, scheduleID);
                pstmt.setString(4, name);
                pstmt.setString(5, type.toString());
                pstmt.setString(6, os);
                pstmt.executeUpdate();
            }
        }      
    }
    
    @Override
    public String getOS() {
        return this.OS;
    }
}