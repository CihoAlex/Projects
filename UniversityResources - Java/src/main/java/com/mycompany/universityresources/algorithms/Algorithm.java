/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.algorithms;

/**
 * lines with rooms, columns with hours and name of event on position
 * occupiedRooms[0][12] = C3 ------ event C3 is in room L0 at 12 o'clock
 * @author alexc
 */
public abstract class Algorithm {
    public  String[][] occupiedRooms;
    public Problem pb;
    
    public Algorithm()
    {
      
    }

    public void setOccupiedRooms(String[][] occupiedRooms) {
        this.occupiedRooms = occupiedRooms;
    }

    public void setPb(Problem pb) {
        this.pb = pb;
    }
   
}
