package com.mycompany.universityresources.algorithms;

import com.mycompany.universityresources.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class Solution {
  
    public Solution(String[][] occupiedRooms, int scheduleID, Problem pb) throws SQLException {
        Connection con = Database.getConnection();
        for (int i = 0; i < occupiedRooms.length; i++) {
            for (int j = 1; j < (occupiedRooms[0]).length; j++) {
                if (occupiedRooms[i][j] != null) {
                    Statement stmt = con.createStatement();
                    try {
                        ResultSet rs = stmt.executeQuery("SELECT nextval('idcolumn')");
                        try {
                            int count = (rs.next() ? Integer.valueOf(rs.getInt(1)) : null).intValue();
                            PreparedStatement pstmt = con.prepareStatement("insert into public.schedules (id, schedule,eventname,eventsize,eventstart,eventend,room,roomcapacity, time) values (?,?,?,?,?,?,?,?,?)");
                            try {
                                pstmt.setInt(1, count);
                                pstmt.setInt(2, scheduleID);
                                pstmt.setString(3, pb.getEventbyName(occupiedRooms[i][j]));
                                pstmt.setInt(4, pb.getEventSize(occupiedRooms[i][j]));
                                pstmt.setInt(5, pb.getEventStart(occupiedRooms[i][j]).getHour());
                                pstmt.setInt(6, pb.getEventEnd(occupiedRooms[i][j]).getHour());
                                pstmt.setString(7, pb.rooms[i].getName());
                                pstmt.setInt(8, pb.rooms[i].getCap());
                                pstmt.setInt(9, j);
                                pstmt.executeUpdate();
                                if (pstmt != null)
                                    pstmt.close(); 
                            } catch (SQLException throwable) {
                            if (pstmt != null)
                                try {
                                    pstmt.close();
                                } catch (SQLException throwable1) {
                                    throwable.addSuppressed(throwable1);
                                }  
                                throw throwable;
                            } 
                            if (rs != null)
                                rs.close(); 
                        } catch (Throwable throwable) {
                        if (rs != null)
                            try {
                                rs.close();
                            } catch (SQLException throwable1) {
                                throwable.addSuppressed(throwable1);
                            }  
                            throw throwable;
                        } 
                        if (stmt != null)
                            stmt.close(); 
                    } catch (Throwable throwable) {
                    if (stmt != null)
                        try {
                            stmt.close();
                        } catch (Throwable throwable1) {
                            throwable.addSuppressed(throwable1);
                        }  
                        throw throwable;
                    } 
                } 
            } 
        } 
    }
}
