/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.objects;

import java.sql.*;

import static com.mycompany.universityresources.objects.EventType.EVENT_COURSE;
import com.mycompany.universityresources.Database;
/**
 * @author alexc
 */
public class Course extends Event {
    private boolean needsVideoProjector;
    
    public Course(String name, int size, int startTime, int endTime, int scheduleID, int needsVideoProjector) throws SQLException {
        super(name, size, startTime, endTime, scheduleID);
        this.setType(EVENT_COURSE);
    
        if (needsVideoProjector == 0)
            this.needsVideoProjector = false;
        else if (needsVideoProjector == 1)
            this.needsVideoProjector = true;
        
        Connection con = Database.getConnection();
        try (Statement stmt = con.createStatement();
             ResultSet rs = stmt.executeQuery(
               "SELECT nextval('eventsid')")) {
            this.setId(  rs.next() ? rs.getInt(1) : null);
            try (PreparedStatement pstmt = con.prepareStatement(
                    "insert into public.events (id, name,size, startTime,endTime, scheduleID,type) values (?,?,?,?,?,?,?)")) {
                pstmt.setInt(1, this.getId());
                pstmt.setString(2, name);
                pstmt.setInt(3, size);
                pstmt.setInt(4, this.getStartTime().getHour());
                pstmt.setInt(5, this.getEndTime().getHour());
                pstmt.setInt(6, scheduleID);
                pstmt.setString(7, this.getType().toString());
                pstmt.executeUpdate();
            }
        }
    }
    
    @Override
    public boolean getVideoProjector() {
        return this.needsVideoProjector;
    }
    
    @Override
    public String getVideo() {
        return this.needsVideoProjector ? "Needs video projector" : "doesn't needs video proejctor";
    }
}