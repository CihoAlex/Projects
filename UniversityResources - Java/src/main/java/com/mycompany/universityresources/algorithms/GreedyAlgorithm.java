/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package com.mycompany.universityresources.algorithms;

import static com.mycompany.universityresources.objects.EventType.EVENT_COURSE;
import static com.mycompany.universityresources.objects.EventType.EVENT_LABORATOR;
import static com.mycompany.universityresources.objects.RoomType.ROOM_COMPUTER_LAB;
import static com.mycompany.universityresources.objects.RoomType.ROOM_LECTURE_HALL;
import  com.mycompany.universityresources.objects.*;
import javax.swing.JLabel;

/**
 * @author alexc
 */

public class GreedyAlgorithm extends Algorithm{
    

    public GreedyAlgorithm(Problem pb) {
        this.pb = pb;
        this.occupiedRooms =  new String[this.pb.countRooms][20];
    }
    
    public String[][] solve() {
        
        
        for (Event event : pb.events) {
            int bestRoom=-1;
            int greedy=999999;
            for (int j = 0; j < pb.rooms.length; j++) {
                if (pb.rooms[j].getCap() >= event.getSize()) {
                    if (occupiedRooms[j][event.getStartTime().getHour()] == null) {
                        if (pb.rooms[j].getType() == ROOM_COMPUTER_LAB && event.getType() == EVENT_LABORATOR) {
                            if (pb.rooms[j].getOS().equals(event.getOS())) {
                                if ( (pb.rooms[j].getCap() - event.getSize()) < greedy) {
                                    greedy = event.getSize() - pb.rooms[j].getCap();
                                     bestRoom=j;
                                }
                            }
                        } else if (pb.rooms[j].getType() == ROOM_LECTURE_HALL) {
                            if (event.getType() == EVENT_COURSE) {
                                if (pb.rooms[j].getVideoProjector() == event.getVideoProjector()) {
                                   if ( (pb.rooms[j].getCap() - event.getSize()) < greedy) {
                                        greedy = event.getSize() - pb.rooms[j].getCap();
                                        bestRoom=j;
                                    }   
                                }
                            }
                        }
                    }
                }
            }
            if (bestRoom != -1) {
                if (occupiedRooms[bestRoom][event.getStartTime().getHour()]==null) {
                    occupiedRooms[bestRoom][event.getStartTime().getHour()] = event.getName();
                    event.setIsAssigned("true");
                    int hoursOfevent = event.getEndTime().getHour() - event.getStartTime().getHour();
                    while (hoursOfevent > 1) {
                        occupiedRooms[bestRoom][event.getStartTime().getHour() + hoursOfevent - 1] = event.getName();
                        hoursOfevent--;    
                    }
                }
            }
        }        
    return occupiedRooms;  
    }
    public void checkSolution(JLabel jlabel) {
        for (var event : pb.events) {
            if (!event.getIsAssigned().equals("true")) {
                jlabel.setVisible(true);
                jlabel.setText(jlabel.getText() + event + " nu are room! ");
            }
        }
    }
}