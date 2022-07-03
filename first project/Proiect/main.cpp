#include <iostream>
#include <graphics.h>
#include <winbgim.h>
#include <string.h>
#include <windows.h>
#include <mmsystem.h>
#include <fstream>
#include <stdlib.h>

#define v initialMatrix
#define stangaSus 1
#define sus 2
#define dreaptaSus 3
#define dreapta 4
#define dreaptaJos 5
#define jos 6
#define stangaJos 7
#define stanga 8


using namespace std;

struct cpuPawn
{
    int currentx;
    int currenty;
    int finalx;
    int finaly;
};

int winMatrix[8][8]= {{0,0,0,0,0,1,1,1},{0,0,0,0,0,0,1,1},{0,0,0,0,0,0,0,1},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{2,0,0,0,0,0,0,0},{2,2,0,0,0,0,0,0},{2,2,2,0,0,0,0,0}};
int startMatrix[8][8]= {{0,0,0,0,0,2,2,2},{0,0,0,0,0,0,2,2},{0,0,0,0,0,0,0,2},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{1,0,0,0,0,0,0,0},{1,1,0,0,0,0,0,0},{1,1,1,0,0,0,0,0}};
int initialMatrix[8][8] = {{0,0,0,0,0,2,2,2},{0,0,0,0,0,0,2,2},{0,0,0,0,0,0,0,2},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{1,0,0,0,0,0,0,0},{1,1,0,0,0,0,0,0},{1,1,1,0,0,0,0,0}};
char tableMatrix[8][8][3] = {{"a1","b1","c1","d1","e1","f1","g1","h1"},{"a2","b2","c2","d2","e2","f2","g2","h2"},{"a3","b3","c3","d3","e3","f3","g3","h3"},{"a4","b4","c4","d4","e4","f4","g4","h4"},{"a5","b5","c5","d5","e5","f5","g5","h5"},{"a6","b6","c6","d6","e6","f6","g6","h6"},{"a7","b7","c7","d7","e7","f7","g7","h7"},{"a8","b8","c8","d8","e8","f8","g8","h8"}};
int player1Color = -1;
int player2Color = -1;
bool gameStart = false;
bool pvpActive = false;
bool helpActive = false;
int platform;
int jumped = 0;
int scor1=0;
int scor2 =0;int r=0;
bool soundActive = 1;
int poz = 150;
int exitGame = 0;
int load = 0;
cpuPawn cpu1[6];
cpuPawn cpu2[6];
bool pvcActive = false;
bool cvcActive = false;
bool cpuvcpuGameStart = false;
bool cpuGameStart = false;
int t=1;
int h;
bool checkWinPlayer1();
bool checkWinPlayer2();
void credits(int x,int y);
void placePawns();
void placePawns(int midx, int midy, int scale, char playerColor1, char playerColor2);
bool checkClickOnButton(int x, int y, int midx, int midy,int scale);
void createButton (int x, int y, int scale, char name[20], char color);
void createPawn(int x, int y, int scale, char color);
void createTable(int x, int y, int scale);
void movement(int startx, int starty, int endx, int endy,int randPlayer);
void playBackgroundMusic();


bool checkFinishGame(int initMatrix[8][8], int winMatrix[8][8]) //Alex
{
    int i,j;
    for(i=0; i<=7; i++)
    {
        for(j=0; j<=7; j++)
        {
            if(v[i][j]!=winMatrix[i][j])
                return false;

        }
    }
    return true;
}

void restartMatrix(int initMatrix[8][8], int startMatrix[8][8]) //Alex
{
    int i,j;
    for(i=0; i<=7; i++)
    {
        for(j=0; j<=7; j++)
        {
            initMatrix[i][j]=startMatrix[i][j];
        }
    }
    placePawns(1000,360,2,player1Color,player2Color);

}

void createArrow(int x, int y,int x2, int y2, int color, int directie, int scale ) //Alex
{
    setcolor(color);

    if(directie==stangaSus)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2,y2+scale);
        line(x2,y2,x2+scale,y2);
    }
    else if(directie==sus)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2-scale,y2+scale);
        line(x2,y2,x2+scale,y2+scale);
    }
    else if(directie==dreaptaSus)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2-scale,y2);
        line(x2,y2,x2,y2+scale);
    }
    else if(directie==dreapta)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2-scale,y2-scale);
        line(x2,y2,x2-scale,y2+scale);
    }
    else if(directie==dreaptaJos)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2,y2-scale);
        line(x2,y2,x2-scale,y2);
    }
    else if(directie==jos)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2-scale,y2-scale);
        line(x2,y2,x2+scale,y2-scale);
    }
    else if(directie==stangaJos)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2,y2-scale);
        line(x2,y2,x2+scale,y2);
    }
    else if(directie==stanga)
    {
        line(x,y,x2,y2);
        line(x2,y2,x2+scale,y2-scale);
        line(x2,y2,x2+scale,y2+scale);
    }
}

void smileyFace(int x, int y) //Alex
{
    setcolor(YELLOW);
    setfillstyle(1,YELLOW);
    circle(x, y, 15);
    floodfill(x,y,YELLOW);
    setcolor(BLACK);
    setlinestyle(0,0,2);
    line(x-5,y-5,x-4,y-5);
    line(x+5,y-5,x+4,y-5);
    arc(x,y,180,360,10);
}

void helpButton(int x, int y,int midx,int midy,int scale) //Alex
{

    if(checkClickOnButton(x,y,midx,midy,scale))
    {
        initwindow(1000,720,"Help");
        helpActive = true;

        setfillstyle(1,CYAN);
        floodfill(1,1,CYAN);
        setbkcolor(CYAN);

        setcolor(WHITE);
        settextstyle(10,0,6);
        outtextxy(50,30,"Reguli:");

        setcolor(WHITE);
        settextstyle(10,0,1);

        outtextxy(20,100," Jocul consta in deplasarea propriilor pioni");
        outtextxy(20,120,"in coltul opus, in locul pionilor adversi");
        outtextxy(20,140,"in cat mai putine mutari.");
        outtextxy(20,180," Jucatorii au dreptul si obligatia, alternativ,");
        outtextxy(20,200,"la cate o  mutare cu unul dintre pionii proprii.");
        outtextxy(20,220,"Atunci cand ii vine randul sa mute, jucatorul ");
        outtextxy(20,240,"poate deplasa oricare dintre pionii sai, prin pas simplu");
        outtextxy(20,260,"sau saritura");
        outtextxy(20,300," Deplasarea  prin saritura consta in saltul pionului peste");
        outtextxy(20,320,"un alt pion, indiferent de culoarea acstuia din urma." );
        outtextxy(20,360," Este indicat ca piesele sa fie dirijate spre coltul opus" );
        outtextxy(20,380,"cat mai grupate (dar nu inghesuite!), astfel incat sa se" );
        outtextxy(20,400,"ajute unele pe altele in inaintare." );
        outtextxy(20,440," Have Fun!");
        smileyFace(150,440);

        createButton(150,650,2,"INAPOI",GREEN);

        createPawn(15,105,1,RED);
        createPawn(15,185,1,RED);
        createPawn(15,305,1,RED);
        createPawn(15,365,1,RED);
        createPawn(15,445,1,RED);
        createTable(850,240,1);
        setbkcolor(CYAN);
        outtextxy(780,380,"Mutarea simpla");
        createPawn(838,252,1,RED);

        createArrow(838,252,838,227,RED,sus, 5);
        createArrow(838,252,863,227,RED,dreaptaSus, 5);
        createArrow(838,252,813,252,RED,stanga, 5);
        createArrow(838,252,813,277,RED,stangaJos, 5);
        createArrow(838,252,838,277,RED,jos, 5);
        createArrow(838,252,863,277,RED,dreaptaJos, 5);
        createArrow(838,252,863,252,RED,dreapta, 5);
        createArrow(838,252,813,225,RED,stangaSus, 5);

        createTable(850,540,1);
        setbkcolor(CYAN);
        outtextxy(720,675,"Mutarea prin saritura");
        createPawn(863,527,1,BLUE);//dreaptaSus
        createPawn(863,577,1,BLUE);//dreaptaJos
        createPawn(863,553,1,BLUE); //dreapta
        createPawn(838,577,1,BLUE);//Jos
        createPawn(813,577,1,BLUE);//stangaJos
        createPawn(813,552,1,BLUE);//stanga
        createPawn(813,527,1,BLUE);//stangaSus
        createPawn(838,527,1,RED);//sus

        createPawn(838,552,1,RED);//mijloc

        createArrow(838,552,838,502,RED,sus,5);
        createArrow(838,552,888,502,RED,dreaptaSus,5);
        createArrow(838,552,888,552,RED,dreapta,5);
        createArrow(838,552,888,602,RED,dreaptaJos,5);
        createArrow(838,552,838,602,RED,jos,5);
        createArrow(838,552,788,602,RED,stangaJos,5);
        createArrow(838,552,788,552,RED,stanga,5);
        createArrow(838,552,788,502,RED,stangaSus,5);


    }
}

bool verificaMutareCorecta2(int linIn, int colIn, int linFin, int colFin) //Alex
{
    if((v[linFin][colFin]>-1)&&(v[linFin][colFin]<8))
        if(v[linFin][colFin]==0&&v[linIn][colIn]!=0)
            if((linFin==linIn-1) && (colFin==colIn-1)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn-1) && (colFin==colIn)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn-1) && (colFin==colIn+1)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn)   && (colFin==colIn+1)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn+1) && (colFin==colIn+1)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn+1) && (colFin==colIn)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn+1) && (colFin==colIn-1)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn)   && (colFin==colIn-1)&&jumped==0)
            {

                return true;
            }
            else if((linFin==linIn-2) && (colFin==colIn-2) && v[linIn-1][colIn-1]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn-2) && (colFin==colIn)&& v[linIn-1][colIn]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn-2) && (colFin==colIn+2)&& v[linIn-1][colIn+1]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn)   && (colFin==colIn+2)&& v[linIn][colIn+1]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn+2) && (colFin==colIn+2)&& v[linIn+1][colIn+1]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn+2) && (colFin==colIn)&& v[linIn+1][colIn]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn+2) && (colFin==colIn-2)&& v[linIn+1][colIn-1]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
            else if((linFin==linIn)   && (colFin==colIn-2)&& v[linIn][colIn-1]!=0&&jumped!=2)
            {
                jumped=1;
                return true;
            }
    return false;

}

void createTable(int x, int y, int scale) //Creaza tabla de joc (Mihai)
{
    setcolor(WHITE);
    setlinestyle(0,0,2*scale);
    rectangle(x-100*scale,y-100*scale,x+100*scale,y+100*scale);
    setlinestyle(0,0,1);
    for(int i=x-100*scale; i<=x+100*scale; i+=25*scale)
        line(i,y-100*scale,i,y+100*scale);
    for(int i=y-100*scale; i<=y+100*scale; i+=25*scale)
        line(x-100*scale,i,x+100*scale,i);
    setlinestyle(0,0,3*scale);
    rectangle(x-125*scale,y-125*scale,x+125*scale,y+125*scale);
    int k=1;
    for (int i=y-100*scale+5; i<=y+100*scale; i+=25*scale)
    {
        for(int j=x-100*scale+5; j<=x+100*scale; j+=25*scale)
            if(k==1)
            {
                setfillstyle(1,WHITE);
                floodfill(j,i,WHITE);
                k=0;
            }
            else
            {
                setfillstyle(1,BLACK);
                floodfill(j,i,WHITE);
                k=1;
            }
        k=1-k;
    }
    char s[10];
    k=8;
    setfillstyle(1,DARKGRAY);
    floodfill(x-125*scale+10,y-125*scale+20,WHITE);
    setbkcolor(DARKGRAY);
    settextstyle(10,HORIZ_DIR,scale*2);
    for(int i=y-95*scale; i<=y+87.5*scale; i+=25*scale)
    {
        itoa(k,s,10);
        outtextxy(x-116*scale,i,s);
        k--;

    }
    k=0;
    for(int i=x-93*scale; i<=x+87.5*scale; i+=25*scale)
    {
        char p = (char)(k+65);
        char af[10];
        af[0]=p;
        af[1] = '\0';
        outtextxy(i,y+103*scale,af);
        k++;
    }
}

void createButton (int x, int y, int scale, char name[20], char color) //Creare buton (Mihai)
{
    setlinestyle(0,0,scale+2);
    setcolor(color);
    rectangle(x-60*scale,y-10*scale,x+60*scale,y+10*scale);
    setfillstyle(1,color+8);
    floodfill(x,y,color);

    setcolor(color);
    setbkcolor(color+8);

    settextstyle(10,HORIZ_DIR,scale+1);
    int lung=strlen(name);
    if(lung%2==0)
        outtextxy(x-(lung/2)*7*scale,y-5*scale,name);
    else
        outtextxy(x-(lung/2)*8*scale,y-5*scale,name);

}

void createPawn(int x,int y, int scale, char color) //Creare Pioni (Mihai)
{
    setcolor(color+8);
    setlinestyle(0,0,scale+1);
    circle(x,y,10*scale);
    setfillstyle(1,color);
    floodfill(x,y,color+8);
    setcolor(color+8);
    setfillstyle(1,color + 8);
    circle(x,y,5*scale);
    floodfill(x,y,color + 8);
}

bool checkClickOnTable(int x,int y, int scale, int midx, int midy) //Verificare click pe tabla(Mihai)
{
    return (x>midx-100*scale&&x<midx+100*scale&&y>midy-100*scale&&y<midy+100*scale);

}

void convertClickTable(int &x, int &y, int scale, int midx, int midy) //Converteste click in pozitii matrice (Mihai)
{
    if(checkClickOnTable(x,y,scale,midx,midy))
    {
        x=(x-(midx-100*scale))/(25*scale);
        y=(y-(midy-100*scale))/(25*scale);
    }
}

void placePawns(int midx, int midy, int scale, char playerColor1, char playerColor2) //Aseaza pionii pe tabla (Mihai)
{
    for(int i = 0; i<=7; i++)
        for(int j=0; j<=7; j++)
        {
            if (initialMatrix[i][j] == 1)
                createPawn(midx-(87.5*scale)+(j*scale*25),midy-(87.5*scale)+(i*scale*25),scale,playerColor1);
            if(initialMatrix[i][j]==2)
                createPawn(midx-87.5*scale+j*scale*25,midy-87.5*scale+i*scale*25,scale,playerColor2);
            if(initialMatrix[i][j] == 0)
            {
                int color = getpixel(midx-100*scale+4+j*25*scale,midy-100*scale+4+i*25*scale);
                setfillstyle(1,color);
                floodfill(midx-100*scale+25+j*25*scale,midy-100*scale+25+i*25*scale,color);
            }
        }
}

void decodeTableToMatrix(char code[10],int &firstx, int &firsty,int &lastx, int &lasty) //Decodeaza din tabla in matrice (Mihai)
{
    firstx=(int)code[0]-97;
    firsty=8-atoi(&code[1]);
    lastx=(int)code[3]-97;
    lasty=8-atoi(&code[4]);
}

char* decodeMatrixToTable(int firstx, int firsty,int lastx, int lasty)//Decodeaza din matrice in tabla (Mihai)
{
    char code[10];
    /*
    char x = (char)(firstx+97);
    int len = strlen(code);
    code[len] = x;
    code[len+1] = '\0';
    char k[10];
    itoa(8-firsty,k,10);
    len = strlen(code);
    code[len] = k[0];
    code[len+1] = '\0';

    //code[2] = '-';
    /*   strcat(code,(char*)(lastx+97));
       itoa(8-lasty,k,10);
       strcat(code,k); */
    return code;
}

void scoreDisplay(int midx,int midy) //Mihai
{
    setlinestyle(0,0,4);
    setcolor(LIGHTGRAY);
    rectangle(midx-200,midy-30,midx+200,midy+30);
    setfillstyle(1,DARKGRAY);
    floodfill(midx,midy,LIGHTGRAY);
}

void changeScore(int midx,int midy) //Mihai
{
    settextstyle(10,HORIZ_DIR,2);
    setbkcolor(DARKGRAY);
    setfillstyle(1,DARKGRAY);
    floodfill(midx,midy,LIGHTGRAY);
    setcolor(WHITE);
    outtextxy(midx-180,midy-12,"Player  :");
    outtextxy(midx+50,midy-12,"Player  :");
    createPawn(midx-95,midy,1,player1Color);
    createPawn(midx+135,midy,1,player2Color);
    setcolor(WHITE);
    char p[3];
    itoa(scor1,p,10);
    outtextxy(midx-70,midy-10,p);
    itoa(scor2,p,10);
    outtextxy(midx+160,midy-10,p);

}

void soundBox(int midx,int midy,bool mode) //Mihai
{
    playBackgroundMusic();
    setlinestyle(0,0,5);
    setcolor(WHITE);
    rectangle(midx-25,midy-25,midx+25,midy+25);
    setfillstyle(1,LIGHTGRAY);
    floodfill(midx,midy,WHITE);
    if (mode == 0)
    {
        setfillstyle(1,LIGHTGRAY);
        floodfill(midx,midy,WHITE);
        setcolor(BLACK);
        setlinestyle(0,0,3);
        rectangle(midx-17,midy-7,midx-15,midy+7);
        line(midx-17,midy-7,midx-5,midy-15);
        line(midx-17,midy+7,midx-5,midy+15);
        rectangle(midx-5,midy-15,midx-4,midy+15);
        setfillstyle(1,BLACK);
        floodfill(midx-15,midy,BLACK);
        setlinestyle(0,0,3);
        arc(midx+3,midy,-65,65,15);
        arc(midx,midy,-65,65,10);
    }
    if(mode == 1)
    {
        setfillstyle(1,LIGHTGRAY);
        floodfill(midx,midy,WHITE);
        setcolor(BLACK);
        setlinestyle(0,0,3);
        rectangle(midx-17,midy-7,midx-15,midy+7);
        line(midx-17,midy-7,midx-5,midy-15);
        line(midx-17,midy+7,midx-5,midy+15);
        rectangle(midx-5,midy-15,midx-4,midy+15);
        setfillstyle(1,BLACK);
        floodfill(midx-15,midy,BLACK);
        setlinestyle(0,0,3);
        arc(midx+3,midy,-65,65,15);
        arc(midx,midy,-65,65,10);
        line(midx-23,midy-23,midx+23,midy+23);
    }
}

void historyTable(int midx,int midy) //Mihai
{
    setlinestyle(0,0,4);
    setcolor(LIGHTGRAY);
    rectangle(midx-150,midy-220,midx+150,midy+220);
    setfillstyle(1,DARKGRAY);
    floodfill(midx,midy,LIGHTGRAY);
    settextstyle(10,HORIZ_DIR,2);
    setbkcolor(DARKGRAY);
    setcolor(WHITE);
    outtextxy(midx-40,midy-210,"History");

}

void saveBox(int x,int y) //Mihai
{
    initwindow(x,y,"Save",1536/2-x/2,864/2-y/2);
    setfillstyle(1,CYAN);
    floodfill(x/2,y/2,WHITE);
    setcolor(DARKGRAY);
    setlinestyle(0,0,4);
    rectangle(x/2-125,y/2-50,x/2+125,y/2-10);
    setfillstyle(1,LIGHTGRAY);
    floodfill(x/2-40,y/2-40,DARKGRAY);
    createButton(x/2,y/2+30,2,"Save",GREEN);
}

bool checkClickOnPawn(int x, int y, int midx, int midy, int scale) //Mihai
{
    if(x>midx-60*scale&&x<midx+60*scale&&y>midy-10*scale&&y<midy+10*scale&&soundActive&&!gameStart)
        PlaySound(TEXT("button.wav"),NULL,SND_ASYNC);
    return (x>midx-10*scale&&x<midx+10*scale&&y>midy-10*scale&&y<midy+10*scale);
}

bool checkClickOnButton(int x, int y, int midx, int midy, int scale) //Verificare click pe buton (Mihai)
{
    if(x>midx-60*scale&&x<midx+60*scale&&y>midy-10*scale&&y<midy+10*scale&&soundActive)
        PlaySound(TEXT("button.wav"),NULL,SND_ASYNC);
    return (x>midx-60*scale&&x<midx+60*scale&&y>midy-10*scale&&y<midy+10*scale);
}

void exitButton(int x,int y,int scale,int midx, int midy) // Buton exit (Mihai)
{
    if(checkClickOnButton(x,y,midx,midy,scale))
        closegraph(CURRENT_WINDOW);
}

void credits(int x,int y)
{
    setcolor(WHITE);
    setbkcolor(CYAN);
    settextstyle(0,0,0);
    outtextxy(x,y,"© Made by Hristodor Minu-Mihail & Alexandru Cihodaru");
}

void menu()//Meniu (Mihai)
{
    setbkcolor(LIGHTBLUE);
    setfillstyle(1,CYAN);
    floodfill(1,1,WHITE);
    setbkcolor(CYAN);
    setcolor(WHITE);
    settextstyle(10,0,6);
    outtextxy(100,50,"Din colt in colt");
    createTable(1000,360,2);
    createButton(200,200,2,"PLAYER VS PLAYER",BLUE);
    createButton(200,270,2,"PLAYER VS CPU",BLUE);
    createButton(200,340,2,"CPU VS CPU",BLUE);
    createButton(200,410,2,"LOAD",GREEN);
    createButton(200,480,2,"HELP",GREEN);
    createButton(200,550,2,"EXIT",RED);
    credits(750,700);
    soundBox(40,680,0);
}

void playBackgroundMusic() //Mihai
{
    /*if(soundActive)
        PlaySound(TEXT("Background.wav"),NULL,SND_ASYNC|SND_LOOP|SND_NOSTOP);
        else
            PlaySound(NULL,NULL,SND_ASYNC|SND_LOOP); */ //not working properly atm
}

void pvpButton(int x,int y,int scale, int midx,int midy) //Mihai
{
    if (checkClickOnButton(x,y,midx,midy,scale)&&!gameStart)
    {
        pvpActive = true;
        createButton(450,200,2,"COLOR PLAYER 1",BLUE);
        createPawn(450,260,3,RED);
        createPawn(450,330,3,GREEN);
        createButton(450,410,2,"COLOR PLAYER 2",BLUE);
        createPawn(450,470,3,MAGENTA);
        createPawn(450,540,3,BLUE);
    }
}

void endTurn(int &randPlayer) //Mihai
{
    if(randPlayer == 1)
        randPlayer =2;
    else
        randPlayer = 1;
    jumped=0;
}

void highlighter(int &x, int &y) //Mihai
{
    int bg = getpixel(803+x*50,163+y*50);
    setbkcolor(bg);
    setcolor(COLOR(255, 255, 204));
    setfillstyle(1,COLOR(255, 255, 204));
    circle(825+x*50,185+y*50,6);
    floodfill(825+x*50,185+y*50,COLOR(255, 255, 204));

}

void writeHistory(int randPlayer, int poz,int inceputx,int inceputy,int sfarsitx,int sfarsity,int player1Color,int player2Color) //Alex
{
    settextstyle(10,HORIZ_DIR,2);
    setbkcolor(DARKGRAY);
    setcolor(WHITE);

    if( (randPlayer==2) && (poz<=520) )
    {
        outtextxy(410,poz,"  : ");
        createPawn(420,poz+10,1,player1Color);
        setcolor(WHITE);
        if(inceputx==7)
        {
            outtextxy(450,poz,"1");
        }
        else if(inceputx==6)
        {
            outtextxy(450,poz,"2");
        }
        else if(inceputx==5)
        {
            outtextxy(450,poz,"3");
        }
        else if(inceputx==4)
        {
            outtextxy(450,poz,"4");
        }
        else if(inceputx==3)
        {
            outtextxy(450,poz,"5");
        }
        else if(inceputx==2)
        {
            outtextxy(450,poz,"6");
        }
        else if(inceputx==1)
        {
            outtextxy(450,poz,"7");
        }
        else if(inceputx==0)
        {
            outtextxy(450,poz,"8");
        }


        if(inceputy==0)
        {
            outtextxy(465,poz,"A-");
        }
        else if(inceputy==1)
        {
            outtextxy(465,poz,"B-");
        }
        else if(inceputy==2)
        {
            outtextxy(465,poz,"C-");
        }
        else  if(inceputy==3)
        {
            outtextxy(465,poz,"D-");
        }
        else  if(inceputy==4)
        {
            outtextxy(465,poz,"E-");
        }
        else  if(inceputy==5)
        {
            outtextxy(465,poz,"F-");
        }
        else  if(inceputy==6)
        {
            outtextxy(465,poz,"G-");
        }
        else  if(inceputy==7)
        {
            outtextxy(465,poz,"H-");
        }


        if(sfarsity==7)
        {
            outtextxy(490,poz,"1");
        }
        else if(sfarsity==6)
        {
            outtextxy(490,poz,"2");
        }
        else if(sfarsity==5)
        {
            outtextxy(490,poz,"3");
        }
        else  if(sfarsity==4)
        {
            outtextxy(490,poz,"4");
        }
        else if(sfarsity==3)
        {
            outtextxy(490,poz,"5");
        }
        else if(sfarsity==2)
        {
            outtextxy(490,poz,"6");
        }
        else if(sfarsity==1)
        {
            outtextxy(490,poz,"7");
        }
        else  if(sfarsity==0)
        {
            outtextxy(490,poz,"8");
        }

        if(sfarsitx==0)
        {
             outtextxy(505,poz,"A");
        }
        else if(sfarsitx==1)
        {
            outtextxy(505,poz,"B");
        }
        else if(sfarsitx==2)
        {
            outtextxy(505,poz,"C");
        }
        else if(sfarsitx==3)
        {
            outtextxy(505,poz,"D");
        }
        else if(sfarsitx==4)
        {
            outtextxy(505,poz,"E");
        }
        else  if(sfarsitx==5)
        {
            outtextxy(505,poz,"F");
        }
        else  if(sfarsitx==6)
        {
            outtextxy(505,poz,"G");
        }
        else  if(sfarsitx==7)
        {
            outtextxy(505,poz,"H");
        }
    }
    if( (randPlayer==1) && (poz<=520) )
    {
        outtextxy(410,poz,"  : ");
        createPawn(420,poz+10,1,player2Color);
        setcolor(WHITE);
        if(inceputx==7)
        {
            outtextxy(450,poz,"1");
        }
        else if(inceputx==6)
        {
            outtextxy(450,poz,"2");
        }
        else if(inceputx==5)
        {
            outtextxy(450,poz,"3");
        }
        else if(inceputx==4)
        {
            outtextxy(450,poz,"4");
        }
        else  if(inceputx==3)
        {
            outtextxy(450,poz,"5");
        }
        else if(inceputx==2)
        {
            outtextxy(450,poz,"6");
        }
        else if(inceputx==1)
        {
            outtextxy(450,poz,"7");
        }
        else  if(inceputx==0)
        {
            outtextxy(450,poz,"8");
        }

        if(inceputy==0)
        {
            outtextxy(465,poz,"A-");
        }
        else  if(inceputy==1)
        {
            outtextxy(465,poz,"B-");
        }
        else  if(inceputy==2)
        {
            outtextxy(465,poz,"C-");
        }
        else  if(inceputy==3)
        {
            outtextxy(465,poz,"D-");
        }
        else  if(inceputy==4)
        {
            outtextxy(465,poz,"E-");
        }
        else  if(inceputy==5)
        {
            outtextxy(465,poz,"F-");
        }
        else   if(inceputy==6)
        {
            outtextxy(465,poz,"G-");
        }
        else  if(inceputy==7)
        {
            outtextxy(465,poz,"H-");
        }


        if(sfarsity==7)
        {
            outtextxy(490,poz,"1");
        }
        else  if(sfarsity==6)
        {
            outtextxy(490,poz,"2");
        }
        else if(sfarsity==5)
        {
            outtextxy(490,poz,"3");
        }
        else if(sfarsity==4)
        {
            outtextxy(490,poz,"4");
        }
        else if(sfarsity==3)
        {
            outtextxy(490,poz,"5");
        }
        else  if(sfarsity==2)
        {
            outtextxy(490,poz,"6");
        }
        else  if(sfarsity==1)
        {
            outtextxy(490,poz,"7");
        }
        else   if(sfarsity==0)
        {
            outtextxy(490,poz,"8");
        }

        if(sfarsitx==0)
        {
            outtextxy(505,poz,"A");
        }
        else  if(sfarsitx==1)
        {
            outtextxy(505,poz,"B");
        }
        else  if(sfarsitx==2)
        {
            outtextxy(505,poz,"C");
        }
        else  if(sfarsitx==3)
        {
            outtextxy(505,poz,"D");
        }
        else if(sfarsitx==4)
        {
            outtextxy(505,poz,"E");
        }
        else if(sfarsitx==5)
        {
            outtextxy(505,poz,"F");
        }
        else if(sfarsitx==6)
        {
            outtextxy(505,poz,"G");
        }
        else  if(sfarsitx==7)
        {
            outtextxy(505,poz,"H");
        }
    }
}

void loadGame(int &randPlayer, int &initialx,int &initialy,bool &selected,int &okUndo) //Mihai
{
    char nume[20];
    cout<<endl;
    cout<<"Introduce-ti numele fisierului de load: ";
    cin>>nume;
    strcat(nume,".txt");
    ifstream f(nume);
    int x;
    f>>x;
    if(x==0);
    gameStart = true;
    f>>player1Color;
    f>>player2Color;
    f>>randPlayer;
    f>>initialx;
    f>>initialy;
    f>>selected;
    f>>jumped;
    f>>okUndo;
    f>>scor1;
    f>>scor2;
    for(int i=0; i<8; i++)
        for(int j=0; j<8; j++)
            f>>v[i][j];

}

void cpuSetup()
{
    cpu1[0].currentx =cpu2[0].finalx =0;
    cpu1[0].currenty = cpu2[0].finaly =5;
    cpu1[0].finalx = cpu2[0].currentx = 5;
    cpu1[0].finaly = cpu2[0].currenty =0;

    cpu1[1].currentx = cpu2[1].finalx =0;
    cpu1[1].currenty = cpu2[1].finaly =6;
    cpu1[1].finalx = cpu2[1].currentx =6;
    cpu1[1].finaly = cpu2[1].currenty =0;

    cpu1[2].currentx = cpu2[2].finalx =0;
    cpu1[2].currenty = cpu2[2].finaly =7;
    cpu1[2].finalx = cpu2[2].currentx =7;
    cpu1[2].finaly = cpu2[2].currenty =0;

    cpu1[3].currentx = cpu2[3].finalx =1;
    cpu1[3].currenty = cpu2[3].finaly =6;
    cpu1[3].finalx = cpu2[3].currentx =6;
    cpu1[3].finaly = cpu2[3].currenty =1;

    cpu1[4].currentx = cpu2[4].finalx =1;
    cpu1[4].currenty = cpu2[4].finaly =7;
    cpu1[4].finalx = cpu2[4].currentx =7;
    cpu1[4].finaly = cpu2[4].currenty =1;

    cpu1[5].currentx = cpu2[5].finalx =2;
    cpu1[5].currenty = cpu2[5].finaly =7;
    cpu1[5].finalx = cpu2[5].currentx =7;
    cpu1[5].finaly = cpu2[5].currenty =2;

}

bool checkWinPlayer2()
{
    return (v[7][0]==2)&&(v[6][0]==2)&&(v[5][0]==2)&&(v[6][1]==2)&&(v[7][1]==2)&&(v[7][2]==2);

}

bool checkWinPlayer1()
{
    return (v[0][7]==1)&&(v[0][6]==1)&&(v[0][5]==1)&&(v[1][6]==1)&&(v[1][7]==1)&&(v[2][7]==1);

}

void interactiveInterface(int &x, int &y)
{

    if(!helpActive)
    {
        if(checkClickOnButton(x,y,200,270,2))
            pvcActive = true;
        if(checkClickOnButton(x,y,200,340,2))
            cvcActive = true;
        if(x>15&&x<65&&y>655&&y<705)
            if(soundActive)
            {
                soundActive= 1-soundActive;
                soundBox(40,680,1);
            }
            else
            {
                soundActive=1-soundActive;
                soundBox(40,680,0);
            }
        helpButton(x,y,200,480,2);
        pvpButton(x,y,2,200,200);
        exitButton(x,y,2,200,550);
        if(checkClickOnButton(x,y,200,410,2)&&!pvpActive)
        {
            load =1;
            gameStart = true;
        }
        if(pvpActive&&!gameStart)
        {
            if(checkClickOnPawn(x,y,450,260,3))
                player1Color = RED;
            if(checkClickOnPawn(x,y,450,330,3))
                player1Color = GREEN;
            if(checkClickOnPawn(x,y,450,470,3))
                player2Color = MAGENTA;
            if(checkClickOnPawn(x,y,450,540,3))
                player2Color = BLUE;
            if(player1Color!=-1&&player2Color!=-1)
            {
                pvpActive = false;
                gameStart = true;
                setfillstyle(1,CYAN);
                floodfill(450,260,CYAN);
                floodfill(450,330,CYAN);
                floodfill(450,470,CYAN);
                floodfill(450,540,CYAN);
                floodfill(450,200,CYAN);
                floodfill(450,410,CYAN);

            }
        }
        if(gameStart)
        {
            if(!load)
            {
                createButton(550,600,2,"SAVE",GREEN);
                createButton(550,650,2,"RESTART",RED);
                scoreDisplay(1000,60);
                changeScore(1000,60);
                historyTable(550,330);
                createButton(875,650,2,"UNDO",LIGHTGRAY);
                placePawns(1000,360,2,player1Color,player2Color);
                createButton(1125,650,2,"END TURN",player1Color);
            }
            bool selected = 0;
            int clickx,clicky;
            int initialx = -1;
            int initialy = -1;
            int randPlayer = 1;
            int okEndTurn = 0;
            int okUndo = 0;
            int lastx,lasty,lastJump;
            int mutari = 0;
            while(gameStart)
            {
                if(load)
                {
                    loadGame(randPlayer,initialx,initialy,selected,okUndo);
                    poz=150;
                    load = 0;
                    createButton(550,600,2,"SAVE",GREEN);
                    createButton(550,650,2,"RESTART",RED);
                    scoreDisplay(1000,60);
                    changeScore(1000,60);
                    historyTable(550,330);
                    createButton(875,650,2,"UNDO",LIGHTGRAY);
                    placePawns(1000,360,2,player1Color,player2Color);
                    createButton(1125,650,2,"END TURN",player1Color);
                    cout<<"Incarcare efectuata"<<endl;
                }
                getmouseclick(WM_LBUTTONDOWN,clickx,clicky);
                if(clickx>15&&clickx<65&&clicky>655&&clicky<705)
                    if(soundActive)
                    {
                        soundActive= 1-soundActive;
                        soundBox(40,680,1);
                    }
                    else
                    {
                        soundActive=1-soundActive;
                        soundBox(40,680,0);
                    }
                if(checkClickOnTable(clickx,clicky,2,1000,360))
                    convertClickTable(clickx,clicky,2,1000,360);
                if(v[clicky][clickx]==randPlayer&&clickx<8&&clicky<8&&initialx == -1&&initialy == -1&&!selected)
                {
                    initialx = clickx;
                    initialy = clicky;
                    highlighter(clickx,clicky);
                }
                else if ((initialx!=-1&&initialy!=-1&&v[clicky][clickx]==randPlayer&&clickx<8&&clicky<8)&&!selected)
                {
                    placePawns(1000,360,2,player1Color,player2Color);
                    initialx = clickx;
                    initialy = clicky;
                    highlighter(clickx,clicky);
                }
                if(verificaMutareCorecta2(initialy,initialx,clicky,clickx)&&v[initialy][initialx] == randPlayer)
                {
                    mutari++;
                    if(randPlayer==1)
                        createButton(875,650,2,"UNDO",player1Color);
                    else
                        createButton(875,650,2,"UNDO",player2Color);
                    okEndTurn=1;
                    movement(initialy,initialx,clicky,clickx,randPlayer);
                    selected =1;
                    lastJump=jumped;
                    if(jumped==0)
                        jumped=2;
                    lastx = initialx;
                    lasty=initialy;
                    initialx=clickx;
                    initialy=clicky;
                    highlighter(clickx,clicky);
                    okUndo=1;
                    if(poz<520)
                    {
                        writeHistory(randPlayer,poz,lasty, lastx,initialx,initialy,player2Color,player1Color);
                    }
                    else
                    {
                        historyTable(550,330);
                        poz=150;
                        writeHistory(randPlayer,poz,lasty,lastx,initialx,initialy,player2Color,player1Color);
                    }

                    if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                    {
                        poz+=30;
                    }
                    else
                    {
                        poz+=30;
                    }
                }
                if(checkWinPlayer2())
                    {okEndTurn = 1;}
                if(checkWinPlayer1())
                    {okEndTurn = 1;}
                if(checkClickOnButton(clickx,clicky,1125,650,2)&&okEndTurn==1)
                {
                    if(randPlayer==1)
                        createButton(1125,650,2,"END TURN",player2Color);
                    else
                        createButton(1125,650,2,"END TURN",player1Color);
                    mutari=0;
                    endTurn(randPlayer);
                    if(checkFinishGame(initialMatrix,winMatrix))
                    {
                        scoreDisplay(1000,60);
                        setcolor(RED);
                        if(scor1>scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Felicitari   ai castigat!");
                            createPawn(965,60,1,player2Color);
                            smileyFace(1180, 60);
                            if(soundActive)
                                PlaySound(TEXT("tada.wav"),NULL,SND_ASYNC);

                        }
                        else if(scor1<scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Felicitari   ai castigat!");
                            createPawn(965,60,1,player1Color);
                            smileyFace(1180, 60);
                            if(soundActive)
                                PlaySound(TEXT("tada.wav"),NULL,SND_ASYNC);

                        }
                        else if(scor1==scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Remiza!");
                        }
                    }

                    okEndTurn = 0;
                    selected = 0;
                    initialx=-1;
                    initialy=-1;
                    placePawns(1000,360,2,player1Color,player2Color);
                    okUndo=0;
                    createButton(875,650,2,"UNDO",LIGHTGRAY);
                }
                if(checkClickOnButton(clickx,clicky,875,650,2)&&okUndo == 1)
                {
                    poz=poz-30;
                    mutari--;
                    if(mutari == 0)
                        okEndTurn = 0;
                    if(randPlayer==1)
                        scor1-=2;
                    else
                        scor2-=2;
                    createButton(875,650,2,"UNDO",LIGHTGRAY);
                    movement(initialy,initialx,lasty,lastx,randPlayer);
                    highlighter(lastx,lasty);
                    initialx = lastx;
                    initialy= lasty;
                    selected = 1;
                    jumped=lastJump;
                    okUndo=0;
                }
                if(checkClickOnButton(clickx,clicky,550,600,2))
                {
                    char name[20];
                    cout<<endl;
                    cout<<"Introdu numele fisierului de salvat: ";
                    cin>>name;
                    cout<<"Salvare efectuata!";
                    strcat(name,".txt");
                    ofstream g(name);
                    g<<0<<endl<<player1Color<<" "<<player2Color<<endl<<randPlayer<<endl<<initialx<<" "<<initialy<<endl
                     <<selected<<endl<<jumped<<endl<<okUndo<<endl<<scor1<<" "<<scor2<<endl;
                    for(int i=0; i<8; i++)
                    {
                        for(int j=0; j<8; j++)
                        {
                            g<<v[i][j]<<" ";
                        }
                        g<<endl;
                    }
                    g.close();
                    cout<<endl;
                }
                if(checkClickOnButton(clickx,clicky,550,650,2))
                {
                    scor1=0;
                    scor2=0;
                    for(int i=0; i<8; i++)
                        for(int j=0; j<8; j++)
                            v[i][j]=startMatrix[i][j];
                    setcolor(CYAN);
                    setfillstyle(1,CYAN);
                    floodfill(550,650,CYAN);
                    floodfill(1000,360,CYAN);
                    floodfill(1000,60,CYAN);
                    floodfill(550,330,CYAN);
                    floodfill(550,600,CYAN);
                    floodfill(1125,650,CYAN);
                    floodfill(875,650,CYAN);
                    pvpActive = 0;
                    gameStart = 0;
                    createTable(1000,360,2);
                    player1Color = -1;
                    player2Color = -1;
                }
            }
        }
        if(pvcActive)
        {
            if(t==1)
            {
                createButton(450,200,2,"COLOR PLAYER 1",BLUE);
                createPawn(450,260,3,RED);
                createPawn(450,330,3,GREEN);
                createPawn(450,400,3,MAGENTA);
                t=0;
            }
            if(checkClickOnPawn(x,y,450,260,3))
                player1Color = RED;
            if(checkClickOnPawn(x,y,450,330,3))
                player1Color = GREEN;
            if(checkClickOnPawn(x,y,450,400,3))
                player1Color = MAGENTA;

            if(player1Color!=-1)
            {
                cpuGameStart = true;
                pvcActive = false;
                setfillstyle(1,CYAN);
                floodfill(450,260,CYAN);
                floodfill(450,330,CYAN);
                floodfill(450,400,CYAN);
                floodfill(450,200,CYAN);
            }
            if(cpuGameStart)
            {
                player2Color = BLUE;
                createButton(550,600,2,"SAVE",GREEN);
                createButton(550,650,2,"RESTART",RED);
                scoreDisplay(1000,60);
                changeScore(1000,60);
                historyTable(550,330);
                createButton(875,650,2,"UNDO",LIGHTGRAY);
                placePawns(1000,360,2,player1Color,player2Color);
                createButton(1125,650,2,"END TURN",player1Color);
                bool selected = 0;
                int clickx,clicky;
                int initialx = -1;
                int initialy = -1;
                int randPlayer = 1;
                int okEndTurn = 0;
                int okUndo = 0;
                int lastx,lasty,lastJump;
                int mutari = 0;
                bool done = false;
                int finished[6] = {};
                cpuSetup();
                poz=150;
                while(cpuGameStart)
                {
                    getmouseclick(WM_LBUTTONDOWN,clickx,clicky);
                    if(clickx>15&&clickx<65&&clicky>655&&clicky<705)
                        if(soundActive)
                        {
                            soundActive= 1-soundActive;
                            soundBox(40,680,1);
                        }
                        else
                        {
                            soundActive=1-soundActive;
                            soundBox(40,680,0);
                        }
                    if(checkClickOnTable(clickx,clicky,2,1000,360))
                        convertClickTable(clickx,clicky,2,1000,360);
                    if(v[clicky][clickx]==randPlayer&&clickx<8&&clicky<8&&initialx == -1&&initialy == -1&&!selected)
                    {
                        initialx = clickx;
                        initialy = clicky;
                        highlighter(clickx,clicky);
                    }
                    else if ((initialx!=-1&&initialy!=-1&&v[clicky][clickx]==randPlayer&&clickx<8&&clicky<8)&&!selected)
                    {
                        placePawns(1000,360,2,player1Color,player2Color);
                        initialx = clickx;
                        initialy = clicky;
                        highlighter(clickx,clicky);
                    }
                    if(verificaMutareCorecta2(initialy,initialx,clicky,clickx)&&v[initialy][initialx] == 1)
                    {
                        mutari++;
                        if(randPlayer==1)
                            createButton(875,650,2,"UNDO",player1Color);
                        else
                            createButton(875,650,2,"UNDO",player2Color);
                        okEndTurn=1;
                        movement(initialy,initialx,clicky,clickx,randPlayer);
                        selected =1;
                        lastJump=jumped;
                        if(jumped==0)
                            jumped=2;
                        lastx = initialx;
                        lasty=initialy;
                        initialx=clickx;
                        initialy=clicky;
                        highlighter(clickx,clicky);
                        okUndo=1;
                        if(poz<520 && randPlayer==1)
                        {
                            writeHistory(randPlayer,poz,lasty, lastx,initialx,initialy,player2Color,player1Color);

                        }
                        else if(poz>=520 && randPlayer==1)
                        {
                            historyTable(550,330);
                            poz=150;
                            writeHistory(randPlayer,poz,lasty, lastx,initialx,initialy,player2Color,player1Color);

                        }
                        if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                        {
                            poz+=30;
                        }
                        else
                        {
                            poz+=30;
                        }
                    }
                    if(checkWinPlayer2())
                        okEndTurn = 1;
                    if(checkWinPlayer1())
                        okEndTurn = 1;
                    if(randPlayer == 2)
                    {
                        if(checkWinPlayer2())
                            done = true;
                        while(!done)
                        {
                            int random;
                            jumped =0;
                            random = rand() %6;
                            if(verificaMutareCorecta2(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+2,cpu1[random].currenty-2)&&v[cpu1[random].currentx+2][cpu1[random].currenty-2]==0&&finished[random]==0&&cpu1[random].currenty-2>=0)
                            {
                                done = true;
                                movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+2,cpu1[random].currenty-2,2);
                                cpu1[random].currentx +=2;
                                cpu1[random].currenty -=2;


                                if(poz<520)
                                {
                                    writeHistory(randPlayer,poz,cpu1[random].currenty+1,cpu1[random].currentx-1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                                }
                                else
                                {
                                    historyTable(550,330);
                                    poz=150;
                                    writeHistory(randPlayer,poz,cpu1[random].currenty+1,cpu1[random].currentx-1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                                }
                                if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                                {
                                    poz+=30;
                                }
                                else
                                {
                                    poz+=30;
                                }

                            }
                            if(verificaMutareCorecta2(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+1,cpu1[random].currenty-1)&&v[cpu1[random].currentx+1][cpu1[random].currenty-1]==0&&finished[random]==0&&!done&&cpu1[random].currenty-1>=0)
                            {
                                done = true;
                                movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+1,cpu1[random].currenty-1,2);
                                cpu1[random].currentx +=1;
                                cpu1[random].currenty -=1;

                                if(poz<520)
                                {
                                    writeHistory(randPlayer,poz,cpu1[random].currentx-1,cpu1[random].currenty+1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                                }
                                else
                                {
                                    historyTable(550,330);
                                    poz=150;
                                    writeHistory(randPlayer,poz,cpu1[random].currentx-1,cpu1[random].currenty+1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);

                                }
                                if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                                {
                                    poz+=30;
                                }
                                else
                                {
                                    poz+=30;
                                }

                            }
                            if(cpu1[random].currentx==cpu1[random].finalx&&cpu1[random].currenty==cpu1[random].finaly)
                                finished[random] = 1;
                        }
                    }
                    if(checkWinPlayer1())
                    okEndTurn = 1;
                    if((checkClickOnButton(clickx,clicky,1125,650,2)&&okEndTurn==1)||done)
                    {
                        mutari=0;
                        endTurn(randPlayer);

                        if(checkFinishGame(initialMatrix,winMatrix))
                    {
                        scoreDisplay(1000,60);
                        setcolor(RED);
                        if(scor1>scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Felicitari   ai castigat!");
                            createPawn(950,60,1,player2Color);
                            smileyFace(1180, 60);
                            if(soundActive)
                                PlaySound(TEXT("tada.wav"),NULL,SND_ASYNC);

                        }
                        else if(scor1<scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Felicitari   ai castigat!");
                            createPawn(950,60,1,player1Color);
                            smileyFace(1180, 60);
                            if(soundActive)
                                PlaySound(TEXT("tada.wav"),NULL,SND_ASYNC);

                        }
                        else if(scor1==scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Remiza!");
                        }
                    }

                        done = false;
                        okEndTurn = 0;
                        selected = 0;
                        initialx=-1;
                        initialy=-1;
                        placePawns(1000,360,2,player1Color,player2Color);
                        okUndo=0;
                        createButton(875,650,2,"UNDO",LIGHTGRAY);
                    }
                    if(checkClickOnButton(clickx,clicky,875,650,2)&&okUndo == 1)
                    {
                        poz=poz-30;
                        mutari--;
                        if(mutari == 0)
                            okEndTurn = 0;
                        if(randPlayer==1)
                            scor1-=2;
                        else
                            scor2-=2;
                        createButton(875,650,2,"UNDO",LIGHTGRAY);
                        movement(initialy,initialx,lasty,lastx,randPlayer);
                        highlighter(lastx,lasty);
                        initialx = lastx;
                        initialy= lasty;
                        selected = 1;
                        jumped=lastJump;
                        okUndo=0;
                    }
                    if(checkClickOnButton(clickx,clicky,550,600,2))
                    {
                        char name[20];
                        cout<<endl;
                        cout<<"Introdu numele fisierului de salvat: ";
                        cin>>name;
                        cout<<"Salvare efectuata!";
                        strcat(name,".txt");
                        ofstream g(name);
                        g<<0<<endl<<player1Color<<" "<<player2Color<<endl<<randPlayer<<endl<<initialx<<" "<<initialy<<endl
                         <<selected<<endl<<jumped<<endl<<okUndo<<endl<<scor1<<" "<<scor2<<endl;
                        for(int i=0; i<8; i++)
                        {
                            for(int j=0; j<8; j++)
                            {
                                g<<v[i][j]<<" ";
                            }
                            g<<endl;
                        }
                        g.close();
                        cout<<endl;
                    }
                    if(checkClickOnButton(clickx,clicky,550,650,2))
                    {
                        scor1=0;
                        scor2=0;
                        for(int i=0; i<8; i++)
                            for(int j=0; j<8; j++)
                                v[i][j]=startMatrix[i][j];
                        setcolor(CYAN);
                        setfillstyle(1,CYAN);
                        floodfill(550,650,CYAN);
                        floodfill(1000,360,CYAN);
                        floodfill(1000,60,CYAN);
                        floodfill(550,330,CYAN);
                        floodfill(550,600,CYAN);
                        floodfill(1125,650,CYAN);
                        floodfill(875,650,CYAN);
                        cpuGameStart = false;
                        t=1;
                        pvcActive = 0;
                        pvpActive = 0;
                        gameStart = 0;
                        createTable(1000,360,2);
                        player1Color = -1;
                        player2Color = -1;
                        poz=150;
                    }
                }
            }
        }
        if(cvcActive)
        {
            cpuvcpuGameStart = true;
            cvcActive = false;
        }
        if(cpuvcpuGameStart)
        {
            poz=150;
            player2Color = BLUE;
            player1Color = RED;
            createButton(550,600,2,"SAVE",GREEN);
            createButton(550,650,2,"RESTART",RED);
            scoreDisplay(1000,60);
            changeScore(1000,60);
            historyTable(550,330);
            placePawns(1000,360,2,player1Color,player2Color);
            bool selected = 0;
            int clickx,clicky;
            int initialx = -1;
            int initialy = -1;
            int randPlayer = 1;
            int okEndTurn = 0;
            int okUndo = 0;
            int lastx,lasty,lastJump;
            int mutari = 0;
            bool done = false;
            int finished[6] = {};
            int finished2[6] = {};
            cpuSetup();

            while(cpuvcpuGameStart)
            {
                getmouseclick(WM_LBUTTONDOWN,clickx,clicky);
                if(clickx>15&&clickx<65&&clicky>655&&clicky<705)
                    if(soundActive)
                    {
                        soundActive= 1-soundActive;
                        soundBox(40,680,1);
                    }
                    else
                    {
                        soundActive=1-soundActive;
                        soundBox(40,680,0);
                    }
                if(randPlayer == 2)
                {
                    if(checkWinPlayer2())
                        done = true;
                    int counter = 0;
                    while(!done)
                    {
                        counter++;
                        delay(500);
                        jumped =0;
                        int random;
                        random = rand() %6;
                        random = rand() %6;
                        random = rand() %6;


                        if(verificaMutareCorecta2(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+2,cpu1[random].currenty-2)&&v[cpu1[random].currentx+2][cpu1[random].currenty-2]==0&&finished[random]==0&&cpu1[random].currenty-2>=0)
                        {
                            done = true;
                            movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+2,cpu1[random].currenty-2,2);
                            cpu1[random].currentx +=2;
                            cpu1[random].currenty -=2;

                            if(poz<520)
                            {
                                writeHistory(randPlayer,poz,cpu1[random].currentx-2,cpu1[random].currenty+2,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                            }
                            else
                            {
                                historyTable(550,330);
                                poz=150;
                                writeHistory(randPlayer,poz,cpu1[random].currentx-2,cpu1[random].currenty+2,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                            }
                            if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                            {
                                poz+=30;
                            }
                            else
                            {
                                poz+=30;
                            }

                        }
                        if(verificaMutareCorecta2(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+1,cpu1[random].currenty-1)&&v[cpu1[random].currentx+1][cpu1[random].currenty-1]==0&&finished[random]==0&&cpu1[random].currenty-1>=0&&!done)
                        {
                            done = true;
                            movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+1,cpu1[random].currenty-1,2);
                            cpu1[random].currentx +=1;
                            cpu1[random].currenty -=1;

                            if(poz<520)
                            {
                                writeHistory(randPlayer,poz,cpu1[random].currentx-1,cpu1[random].currenty+1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                            }
                            else
                            {
                                historyTable(550,330);
                                poz=150;
                                writeHistory(randPlayer,poz,cpu1[random].currentx-1,cpu1[random].currenty+1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);

                            }
                            if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                            {
                                poz+=30;
                            }
                            else
                            {
                                poz+=30;
                            }

                        }
                        if(cpu1[random].currentx==cpu1[random].finalx&&cpu1[random].currenty==cpu1[random].finaly)
                            finished[random] = 1;
                        if(counter>5&&!done)
                        {
                            if(verificaMutareCorecta2(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx-2,cpu1[random].currenty+2)&&v[cpu1[random].currentx-2][cpu1[random].currenty+2]==0&&finished[random]==0&&cpu1[random].currenty+2>=0)
                            {
                                done = true;
                                movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx-2,cpu1[random].currenty+2,2);
                                cpu1[random].currentx -=2;
                                cpu1[random].currenty +=2;
                                if(poz<520)
                                {
                                    writeHistory(randPlayer,poz,cpu1[random].currentx-2,cpu1[random].currenty+2,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                                }
                                else
                                {
                                    historyTable(550,330);
                                    poz=150;
                                    writeHistory(randPlayer,poz,cpu1[random].currentx-2,cpu1[random].currenty+2,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                                }
                                if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                                {
                                    poz+=30;
                                }
                                else
                                {
                                    poz+=30;
                                }
                            }
                            if(verificaMutareCorecta2(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx-1,cpu1[random].currenty+1)&&v[cpu1[random].currentx-1][cpu1[random].currenty+1]==0&&finished[random]==0&&cpu1[random].currenty+1>=0&&!done)
                            {
                                done = true;
                                movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx-1,cpu1[random].currenty+1,2);
                                cpu1[random].currentx -=1;
                                cpu1[random].currenty +=1;

                                if(poz<520)
                                {
                                    writeHistory(randPlayer,poz,cpu1[random].currentx-1,cpu1[random].currenty+1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);
                                }
                                else
                                {
                                    historyTable(550,330);
                                    poz=150;
                                    writeHistory(randPlayer,poz,cpu1[random].currentx-1,cpu1[random].currenty+1,cpu1[random].currenty,cpu1[random].currentx,player2Color,player1Color);

                                }
                                if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                                {
                                    poz+=30;
                                }
                                else
                                {
                                    poz+=30;
                                }


                            }
                        }
                    }
                }
                if(randPlayer == 1)
                {
                    if(checkWinPlayer1())
                        done = true;
                    int counter = 0;
                    while(!done)
                    {
                        counter++;
                        delay(500);
                        jumped =0;
                        int random;
                        random = rand() %6;
                        if(verificaMutareCorecta2(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx-2,cpu2[random].currenty+2)&&v[cpu2[random].currentx-2][cpu2[random].currenty+2]==0&&finished2[random]==0&&cpu2[random].currenty+2<=7)
                        {
                            done = true;
                            movement(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx-2,cpu2[random].currenty+2,1);
                            cpu2[random].currentx -=2;
                            cpu2[random].currenty +=2;


                            if(poz<520)
                            {
                                writeHistory(randPlayer,poz,cpu2[random].currentx+2,cpu2[random].currenty-2,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);
                            }
                            else
                            {
                                historyTable(550,330);
                                poz=150;
                                writeHistory(randPlayer,poz,cpu2[random].currentx+2,cpu2[random].currenty-2,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);
                            }
                            if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                            {
                                poz+=30;
                            }
                            else
                            {
                                poz+=30;
                            }
                        }
                        if(verificaMutareCorecta2(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx-1,cpu2[random].currenty+1)&&v[cpu2[random].currentx-1][cpu2[random].currenty+1]==0&&finished2[random]==0&&cpu1[random].currenty+1<=7&&!done)
                        {
                            done = true;
                            movement(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx-1,cpu2[random].currenty+1,1);
                            cpu2[random].currentx -=1;
                            cpu2[random].currenty +=1;

                            if(poz<520)
                            {
                                writeHistory(randPlayer,poz,cpu2[random].currentx+1,cpu2[random].currenty-1,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);
                            }
                            else
                            {
                                historyTable(550,330);
                                poz=150;
                                writeHistory(randPlayer,poz,cpu2[random].currentx+1,cpu2[random].currenty-1,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);

                            }
                            if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                            {
                                poz+=30;
                            }
                            else
                            {
                                poz+=30;
                            }

                        }
                        if(cpu2[random].currentx==cpu2[random].finalx&&cpu2[random].currenty==cpu2[random].finaly)
                            finished2[random] = 1;
                        if(counter>5&&!done)
                        {
                            if(verificaMutareCorecta2(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx+2,cpu2[random].currenty-2)&&v[cpu2[random].currentx+2][cpu2[random].currenty-2]==0&&finished2[random]==0&&cpu2[random].currenty-2>=0)
                            {
                                done = true;
                                movement(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx+2,cpu2[random].currenty-2,1);
                                cpu2[random].currentx +=2;
                                cpu2[random].currenty -=2;


                                if(poz<520)
                                {
                                    writeHistory(randPlayer,poz,cpu2[random].currentx+2,cpu2[random].currenty-2,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);
                                }
                                else
                                {
                                    historyTable(550,330);
                                    poz=150;
                                    writeHistory(randPlayer,poz,cpu2[random].currentx+2,cpu2[random].currenty-2,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);
                                }
                                if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                                {
                                    poz+=30;
                                }
                                else
                                {
                                    poz+=30;
                                }

                            }
                            if(verificaMutareCorecta2(cpu2[random].currentx,cpu2[random].currenty,cpu2[random].currentx+1,cpu2[random].currenty-1)&&v[cpu2[random].currentx+1][cpu2[random].currenty-1]==0&&finished2[random]==0&&cpu2[random].currenty-1>=0&&!done)
                            {
                                done = true;
                                movement(cpu1[random].currentx,cpu1[random].currenty,cpu1[random].currentx+1,cpu1[random].currenty-1,1);
                                cpu2[random].currentx +=1;
                                cpu2[random].currenty -=1;
                                if(poz<520)
                                {
                                    writeHistory(randPlayer,poz,cpu2[random].currentx+1,cpu2[random].currenty-1,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);
                                }
                                else
                                {
                                    historyTable(550,330);
                                    poz=150;
                                    writeHistory(randPlayer,poz,cpu2[random].currentx+1,cpu2[random].currenty-1,cpu2[random].currenty,cpu2[random].currentx,player2Color,player1Color);

                                }
                                if( (checkWinPlayer1()==false) && (checkWinPlayer2()==false) )
                                {
                                    poz+=30;
                                }
                                else
                                {
                                    poz+=30;
                                }
                            }
                        }
                    }
                }

                if(done)
                {
                    mutari=0;
                    endTurn(randPlayer);

                    if(checkFinishGame(initialMatrix,winMatrix))
                    {
                        scoreDisplay(1000,60);
                        setcolor(RED);
                        if(scor1>scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Felicitari   ai castigat!");
                            createPawn(950,60,1,player2Color);
                            smileyFace(1180, 60);
                            if(soundActive&&t==1)
                                {PlaySound(TEXT("tada.wav"),NULL,SND_ASYNC);t=0;}

                        }
                        else if(scor1<scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Felicitari   ai castigat!");
                            createPawn(950,60,1,player1Color);
                            smileyFace(1180, 60);
                            if(soundActive&&t==1)
                                {PlaySound(TEXT("tada.wav"),NULL,SND_ASYNC);t=0;}

                        }
                        else if(scor1==scor2)
                        {
                            setcolor(WHITE);
                            setbkcolor(DARKGRAY);
                            outtextxy(810,50,"Remiza!");
                        }
                    }

                    done = false;
                    okEndTurn = 0;
                    selected = 0;
                    initialx=-1;
                    initialy=-1;
                    placePawns(1000,360,2,player1Color,player2Color);
                    okUndo=0;
                }
                if(checkClickOnButton(clickx,clicky,550,600,2))
                {
                    char name[20];
                    cout<<endl;
                    cout<<"Introdu numele fisierului de salvat: ";
                    cin>>name;
                    cout<<"Salvare efectuata!";
                    strcat(name,".txt");
                    ofstream g(name);
                    g<<0<<endl<<player1Color<<" "<<player2Color<<endl<<randPlayer<<endl<<initialx<<" "<<initialy<<endl
                     <<selected<<endl<<jumped<<endl<<okUndo<<endl<<scor1<<" "<<scor2<<endl;
                    for(int i=0; i<8; i++)
                    {
                        for(int j=0; j<8; j++)
                        {
                            g<<v[i][j]<<" ";
                        }
                        g<<endl;
                    }
                    g.close();
                    cout<<endl;
                }
                if(checkClickOnButton(clickx,clicky,550,650,2))
                {
                    scor1=0;
                    scor2=0;
                    for(int i=0; i<8; i++)
                        for(int j=0; j<8; j++)
                            v[i][j]=startMatrix[i][j];
                    setcolor(CYAN);
                    setfillstyle(1,CYAN);
                    floodfill(550,650,CYAN);
                    floodfill(1000,360,CYAN);
                    floodfill(1000,60,CYAN);
                    floodfill(550,330,CYAN);
                    floodfill(550,600,CYAN);
                    floodfill(1125,650,CYAN);
                    floodfill(875,650,CYAN);
                    cpuGameStart = false;
                    t=1;
                    cvcActive = false;
                    cpuvcpuGameStart = false;
                    pvcActive = 0;
                    pvpActive = 0;
                    gameStart = 0;
                    createTable(1000,360,2);
                    player1Color = -1;
                    player2Color = -1;
                }
            }
        }
    }
    if(helpActive)
        if(checkClickOnButton(x,y,150,650,2))
        {
            closegraph(CURRENT_WINDOW);
            helpActive = false;
            setcurrentwindow(platform);
        }

}

void movement(int startx, int starty, int endx, int endy,int randPlayer) //Mihai
{
    if(randPlayer==1)
        scor1++;
    else
        scor2++;
    v[endx][endy] = v[startx][starty];
    v[startx][starty] = 0;
    changeScore(1000,60);
    placePawns(1000,360,2,player1Color,player2Color);
    /*  for(int i =0 ;i<8;i++)
      {
          for(int j=0;j<8;j++)
              cout<<v[i][j]<<" ";
          cout<<endl;
      }
      cout<<endl; */
    if(soundActive)
        PlaySound(TEXT("move.wav"),NULL,SND_ASYNC);
}

int main()
{
    int tableScale = 2;
    platform = initwindow(1280,720,"Din colt in colt",1536/2-1280/2,864/2-720/2);
    menu();
    playBackgroundMusic();
    while(!exitGame)
    {
        int x,y;
        x=0;
        y=0;
        getmouseclick(WM_LBUTTONDOWN,x,y);
        interactiveInterface(x,y);
    }
    getch();
    return 0;
}
