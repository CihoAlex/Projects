/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.objects;
import java.sql.SQLException;
import java.time.LocalTime;

/**
 * @author alexc
 */
public class Event {
    
    private String name;
    private int size;
    private String isAssigned;
    private LocalTime start;
    private LocalTime end;
    private EventType type;
    private int id;
    private final int scheduleID;
    
    /**  
     * 
     */
    public Event(String name, int size, int startTime, int endTime, int scheduleID) throws SQLException {
        this.name = name;
        this.size = size;
        this.start = LocalTime.of(startTime, 00);
        this.end = LocalTime.of(endTime, 00);
        this.isAssigned = "";
        this.scheduleID = scheduleID;
    }

    public int getId() {
        return id;
    }

    public String getIsAssigned() {
        return isAssigned;
    }

    public void setIsAssigned(String isAssigned) {
        this.isAssigned = isAssigned;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void setType(EventType type) {
        this.type = type;
    }
    
    public String getName() {
        return name;
    }
    
    public void setName(String name) {
        this.name = name;
    }
    
    public int getSize() {
        return size;
    }
    
    public void setSize(int size) {
        this.size = size;
    }
    
    public LocalTime getStartTime() {
        return start;
    }
    
    public void setStartTime(int startTime) {
        this.start = LocalTime.of(startTime, 00);
    }
    
    public LocalTime getEndTime() {
        return end;
    }
    
    public void setEndTime(int endTime) {
        this.end = LocalTime.of(endTime, 00);
    }

    public EventType getType() {
        return type;
    }
    
    public String getOS() {
        return null;
    }
    
    public boolean getVideoProjector() {
        return false;
    }
    
    public String getVideo() {
        return null;
    }
    
    @Override
    public String toString() {
                return name + "(size=" + size + ", start=" + start + ", end=" + end + ")";
    }
    
    @Override
    public boolean equals(Object obj) {
        if (!(obj instanceof Event)) {
            return false;
        }
        Event other = (Event) obj;
        return name.equals(other.name);
    }
}