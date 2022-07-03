/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.objects;

import java.sql.SQLException;

/**
 * @author alexc
 */
public abstract class Room {
    protected  int id;
    protected String  name;
    protected int     cap;
    protected int scheduleID;
    RoomType type;
    
    /**  
     * 
     */
    public Room(String name, int capacity, int scheduleID) throws SQLException {
        this.name = name;
        this.cap = capacity;
        this.scheduleID = scheduleID;
    }   
    
    public String getName() {
        return name;
    }
    
     public void setName(String name) {
        this.name = name;
    }
     
    public int getCap() {
        return cap;
    }
    
    public void setCap(int cap) {
        this.cap = cap;
    }
    
    public RoomType getType() {
        return type;
    }
    
    public String getOS() {
        return null;
    }
    
    public boolean getVideoProjector() {
        return false;
    }
    
    public String getVideo(){
        return null;
    }
    @Override
    public String toString() {
        return  name + "(cap" + cap + ")";
    }
    
    @Override
    public boolean equals(Object obj) {
        if (!(obj instanceof Room)) {
            return false;
        }
        Room other = (Room) obj;
        return name.equals(other.name);
    }
}