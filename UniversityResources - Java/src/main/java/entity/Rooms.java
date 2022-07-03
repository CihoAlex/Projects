/*
 * Cihodaru P.C. Alexandru - 3E1
 */
package entity;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;

/**
 *
 * @author alexc
 */
@Entity
@Table(name = "rooms")
@NamedQueries({
    @NamedQuery(name = "Rooms.findAll", query = "SELECT r FROM Rooms r"),
    @NamedQuery(name = "Rooms.findById", query = "SELECT r FROM Rooms r WHERE r.id = :id"),
    @NamedQuery(name = "Rooms.findByCapacity", query = "SELECT r FROM Rooms r WHERE r.capacity = :capacity"),
    @NamedQuery(name = "Rooms.findByScheduleid", query = "SELECT r FROM Rooms r WHERE r.scheduleid = :scheduleid"),
    @NamedQuery(name = "Rooms.findByName", query = "SELECT r FROM Rooms r WHERE r.name = :name"),
    @NamedQuery(name = "Rooms.findByType", query = "SELECT r FROM Rooms r WHERE r.type = :type"),
    @NamedQuery(name = "Rooms.findByVideoprojector", query = "SELECT r FROM Rooms r WHERE r.videoprojector = :videoprojector"),
    @NamedQuery(name = "Rooms.findByOs", query = "SELECT r FROM Rooms r WHERE r.os = :os")})
public class Rooms implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "id")
    private Integer id;
    @Column(name = "capacity")
    private Integer capacity;
    @Column(name = "scheduleid")
    private Integer scheduleid;
    @Column(name = "name")
    private String name;
    @Column(name = "type")
    private String type;
    @Column(name = "videoprojector")
    private Boolean videoprojector;
    @Column(name = "os")
    private String os;

    public Rooms() {
    }

    public Rooms(Integer id) {
        this.id = id;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getCapacity() {
        return capacity;
    }

    public void setCapacity(Integer capacity) {
        this.capacity = capacity;
    }

    public Integer getScheduleid() {
        return scheduleid;
    }

    public void setScheduleid(Integer scheduleid) {
        this.scheduleid = scheduleid;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Boolean getVideoprojector() {
        return videoprojector;
    }

    public void setVideoprojector(Boolean videoprojector) {
        this.videoprojector = videoprojector;
    }

    public String getOs() {
        return os;
    }

    public void setOs(String os) {
        this.os = os;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (id != null ? id.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Rooms)) {
            return false;
        }
        Rooms other = (Rooms) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "entity.Rooms[ id=" + id + " ]";
    }
    
}
