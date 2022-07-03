package com.mycompany.universityresources.algorithms;

import com.mycompany.universityresources.objects.*;
import javax.swing.JLabel;

public class SimpleAlgorithm extends Algorithm {
  public SimpleAlgorithm(Problem pb) {
    this.pb = pb;
    this.occupiedRooms = new String[this.pb.countRooms][24];
  }
  
    public String[][] solve() {
        for (Event event : this.pb.events) {
            int hoursOfevent = event.getEndTime().getHour() - event.getStartTime().getHour();
            for (int j = 0; j < this.pb.rooms.length; j++) {
                if (this.pb.rooms[j].getCap() >= event.getSize() && 
                    this.occupiedRooms[j][event.getStartTime().getHour()] == null)
                if ((this.pb.rooms[j]).getType() == RoomType.ROOM_COMPUTER_LAB && event.getType() == EventType.EVENT_LABORATOR) {
                    if (this.pb.rooms[j].getOS().equals(event.getOS())) {
                        this.occupiedRooms[j][event.getStartTime().getHour()] = event.getName();
                        event.setIsAssigned("true");
                        while (hoursOfevent > 1 && 
                            this.occupiedRooms[j][event.getStartTime().getHour() + hoursOfevent - 1] == null) {
                                this.occupiedRooms[j][event.getStartTime().getHour() + hoursOfevent - 1] = event.getName();
                                hoursOfevent--;
                        } 
                        break;
                    } 
                
                } else if ((this.pb.rooms[j]).getType() == RoomType.ROOM_LECTURE_HALL && 
                    event.getType() == EventType.EVENT_COURSE) {
                    if (!event.getVideoProjector()) {
                        this.occupiedRooms[j][event.getStartTime().getHour()] = event.getName();
                        event.setIsAssigned("true");
                        while (hoursOfevent > 1 && 
                            this.occupiedRooms[j][event.getStartTime().getHour() + hoursOfevent - 1] == null) {
                            this.occupiedRooms[j][event.getStartTime().getHour() + hoursOfevent - 1] = event.getName();
                            hoursOfevent--;
                        } 
                        break;
                    } 
                if (this.pb.rooms[j].getVideoProjector() == event.getVideoProjector()) {
                    this.occupiedRooms[j][event.getStartTime().getHour()] = event.getName();
                    event.setIsAssigned("true");
                    while (hoursOfevent > 1 && 
                        this.occupiedRooms[j][event.getStartTime().getHour() + hoursOfevent - 1] == null) {
                            this.occupiedRooms[j][event.getStartTime().getHour() + hoursOfevent - 1] = event.getName();
                            hoursOfevent--;
                    } 
                    break;
                } 
            }  
        } 
    } 
        return this.occupiedRooms;
}

      public void checkSolution(JLabel jlabel) {
        for (Event event : this.pb.events) {
          if (!event.getIsAssigned().equals("true")) {
            jlabel.setVisible(true);
            jlabel.setText(jlabel.getText() + jlabel.getText() + " nu are room! ");
          } 
        } 
      }
    }
