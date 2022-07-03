/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.objects;

import java.sql.*;

import static com.mycompany.universityresources.objects.RoomType.ROOM_LECTURE_HALL;
import  com.mycompany.universityresources.Database;
/**
* @author alexc
 */
public class LectureHall extends Room {
    private final boolean videoProjector;
    
    public LectureHall(String name, int capacity, int scheduleID, boolean video) throws SQLException {
        super(name, capacity, scheduleID);
        Connection con = Database.getConnection();
        this.videoProjector = video;
        this.type = ROOM_LECTURE_HALL;        
        try (Statement stmt = con.createStatement();
             ResultSet rs = stmt.executeQuery(
               "SELECT nextval('roomid')")) {
            this.id =  rs.next() ? rs.getInt(1) : null;
            try (PreparedStatement pstmt = con.prepareStatement(
                    "insert into public.rooms (id, capacity, scheduleID, name,type,videoprojector) values (?,?,?,?,?,?)")) {
                pstmt.setInt(1, this.id);
                pstmt.setInt(2, capacity);
                pstmt.setInt(3, scheduleID);
                pstmt.setString(4, name);
                pstmt.setString(5, type.toString());
                pstmt.setBoolean(6, video);
                pstmt.executeUpdate();
            }
        } 
    }
    
    @Override
    public boolean getVideoProjector() {
        return this.videoProjector;
    }
    
    @Override
    public String getVideo() {
        return this.videoProjector ? "Have video Projector" : "Doesn't have video Projector";
    }
}