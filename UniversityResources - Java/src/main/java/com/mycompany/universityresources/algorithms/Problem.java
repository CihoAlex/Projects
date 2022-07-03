package com.mycompany.universityresources.algorithms;
import com.mycompany.universityresources.objects.*;
import java.sql.SQLException;
import java.time.LocalTime;
import java.util.Random;

/**
 *
 * @author alexc
 */
public class Problem {
  public Event[] events;
  
  public Room[] rooms;
  
  public int countEvents;
  
  public int countRooms;
  
  public Problem() {
    this.countEvents = 0;
    this.countRooms = 0;
  }
  
    public Problem(int scheduleID, int countRooms, int countEvents) throws SQLException {
      Random start = new Random();
      Random size = new Random();
      Random rand = new Random();
      this.events = new Event[countEvents];
      this.countRooms = countRooms;
      this.countEvents = countEvents;
      this.rooms = new Room[countRooms];
      int minSize = 10;
      int maxSize = 101;
      int i;
      for (i = 0; i < countEvents; i++) {
          int hour = start.nextInt(6);
          if (rand.nextInt(2) == 0) {
              String name = "Lab" + i;
              this.events[i] = new Laborator(name, size.nextInt(maxSize - minSize) + minSize, hour + 7 + hour + 1, hour + 7 + hour + 1 + 2, scheduleID, rand.nextInt(2));
          } else {
              String name = "Course" + i;
              this.events[i] = new Course(name, size.nextInt(maxSize - minSize) + minSize, hour + 7 + hour + 1, hour + 7 + hour + 1 + 2, scheduleID, rand.nextInt(2));
            } 
        } 
          for (i = 0; i < this.rooms.length; i++) {
                if (rand.nextInt(2) == 0) {
                String name = "LabRoom" + i;
                    if (rand.nextInt(2) == 0) {
                        this.rooms[i] = new ComputerLab(name, rand.nextInt(71) + 30, scheduleID, "Windows");
                    } else {
                        this.rooms[i] = new ComputerLab(name, rand.nextInt(71) + 30, scheduleID, "Linux");
                    } 
                } else {
                    String name = "LectureHall" + i;
                    if (rand.nextInt(2) == 0) {
                      this.rooms[i] = new LectureHall(name, rand.nextInt(71) + 30, scheduleID, true);
                    } else {
                      this.rooms[i] = new LectureHall(name, rand.nextInt(71) + 30, scheduleID, false);
                    } 
                } 
            } 
        }

    public String getEventbyName(String name) {
      for (int i = 0; i < this.countEvents; i++) {
        if (this.events[i].getName().equals(name))
          return this.events[i].getName(); 
      } 
      return null;
    }

    public int getEventSize(String name) {
      for (int i = 0; i < this.countEvents; i++) {
        if (this.events[i].getName().equals(name))
          return this.events[i].getSize(); 
      } 
      return 0;
    }

    public LocalTime getEventStart(String name) {
      for (int i = 0; i < this.countEvents; i++) {
        if (this.events[i].getName().equals(name))
          return this.events[i].getStartTime(); 
      } 
      return null;
    }

    public LocalTime getEventEnd(String name) {
      for (int i = 0; i < this.countEvents; i++) {
        if (this.events[i].getName().equals(name))
          return this.events[i].getEndTime(); 
      } 
      return null;
    }

    public String toString() {
      return "Problem{c=" + this.events + ", r=" + this.rooms + "}";
    }
  }
