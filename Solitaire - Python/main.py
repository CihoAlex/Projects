""" Programare in Python
    Proiect ID:20 Solitaire
    Cihodaru P.C Alexandru grupa: 3E2
    sursa folosita pentru proiect: https://arcade.academy/tutorials/card_game/index.html
"""
import random
import arcade

SCREEN_WIDTH = 1024
SCREEN_HEIGHT = 768
SCREEN_TITLE = "SOLITAIRE"

card_values = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"]
CARD_SUITS = ["Clubs", "Hearts", "Spades", "Diamonds"]

card_scale = 0.6

card_latime = 140 * card_scale
card_inaltime = 190 * card_scale

mat_percent_oversize = 1.25
mat_inaltime = int(card_inaltime * mat_percent_oversize)
mat_latime = int(card_latime * mat_percent_oversize)

vertical_margin_percent = 0.10  # distanta dintre mats
horizontal_margin_percent = 0.10

bottom_y = mat_inaltime / 2 + mat_inaltime * vertical_margin_percent

start_x = mat_latime / 2 + mat_latime * horizontal_margin_percent  # unde incepem sa punem in parte stanga

top_y = SCREEN_HEIGHT - mat_inaltime / 2 - mat_inaltime * vertical_margin_percent  # y la linia de sus

middle_y = top_y - mat_inaltime - mat_inaltime * vertical_margin_percent  # y la linia din mijloc

x_spacinng = mat_latime + mat_latime * horizontal_margin_percent  # cat de de[arte e fiecare pachet

card_vertical_offset = card_inaltime * card_scale * 0.3  # distanta dintre cartile puse una peste alta

pile_count = 13
bottom_face_down_pile = 0
bottom_face_up_pile = 1
play_pile_1 = 2
play_pile_2 = 3
play_pile_3 = 4
play_pile_4 = 5
play_pile_5 = 6
play_pile_6 = 7
play_pile_7 = 8
top_pile_1 = 9
top_pile_2 = 10
top_pile_3 = 11
top_pile_4 = 12

FACE_DOWN_IMAGE = ":resources:images/cards/cardBack_red2.png"


class Card(arcade.Sprite):
    """card sprite"""

    def __init__(self, suit, value, numar, scale=1):
        """constructor"""

        self.suit = suit
        self.value = value
        if numar == "A":
            self.numar = 1
        elif numar == "J":
            self.numar = 11
        elif numar == "Q":
            self.numar = 12
        elif numar == "K":
            self.numar = 13
        else:
            self.numar = int(numar)

        # imaginea care o folosim pentru carti:
        self.image_file_name = f":resources:images/cards/card{self.suit}{self.value}.png"
        self.is_face_up = False
        super().__init__(FACE_DOWN_IMAGE, scale)  # calculate_hit_box = False)

    def face_down(self):  # intoarce cartea cu fata in jos
        self.texture = arcade.load_texture(FACE_DOWN_IMAGE)
        self.is_face_up = False

    def face_up(self):  # intoarce cartea cu fata in sus
        self.texture = arcade.load_texture(self.image_file_name)
        self.is_face_up = True

    @property
    def is_face_down(self):  # este cartea cu fata in jos?
        return not self.is_face_up


class MyGame(arcade.Window):

    def __init__(self):
        super().__init__(SCREEN_WIDTH, SCREEN_HEIGHT, SCREEN_TITLE)

        self.card_list = None  # lista de sprite-uri cu toate cardurile

        arcade.set_background_color(arcade.color.BANGLADESH_GREEN)

        self.held_cards = None  # lista cu cartile care le tinem cu mouse-ul

        self.held_cards_original_position = None  # unde erau cartile inainte sa le tragem cu mouse-ul

        self.pile_mat_list = None  # lista de sprite-uri cu toate mat pe care avem carti

        self.piles = None  # lista de liste, fiecare tine niste carti

    def setup(self):

        self.held_cards = []

        self.held_cards_original_position = []

        self.pile_mat_list: arcade.SpriteList = arcade.SpriteList()

        pile = arcade.SpriteSolidColor(mat_latime, mat_inaltime, arcade.csscolor.DARK_OLIVE_GREEN)  # mat-urile de jos
        pile.position = start_x, bottom_y
        self.pile_mat_list.append(pile)

        pile = arcade.SpriteSolidColor(mat_latime, mat_inaltime, arcade.csscolor.DARK_OLIVE_GREEN)
        pile.position = start_x + x_spacinng, bottom_y
        self.pile_mat_list.append(pile)

        for i in range(7):  # cele 7 mat-uri cu carti
            pile = arcade.SpriteSolidColor(mat_latime, mat_inaltime, arcade.csscolor.DARK_OLIVE_GREEN)
            pile.position = start_x + i * x_spacinng, middle_y
            self.pile_mat_list.append(pile)

        for i in range(4):  # cele 4 mat-uri de final
            pile = arcade.SpriteSolidColor(mat_latime, mat_inaltime, arcade.csscolor.DARK_OLIVE_GREEN)
            pile.position = start_x + i * x_spacinng, top_y
            self.pile_mat_list.append(pile)

        self.card_list = arcade.SpriteList()  # lista de sprite-uri cu toate cartile, indiferent unde se afla

        for card_suit in CARD_SUITS:  # creeaza cartile
            for card_value in card_values:
                card = Card(card_suit, card_value, card_value, card_scale)
                card.position = start_x, bottom_y
                self.card_list.append(card)

        for pos1 in range(len(self.card_list)):  # amesteca cartile
            pos2 = random.randrange(len(self.card_list))
            self.card_list[pos1], self.card_list[pos2] = self.card_list[pos2], self.card_list[pos1]

        self.piles = [[] for _ in range(pile_count)]  # lista de liste care tine cate un pachet de carti

        for card in self.card_list:  # pune toate cartile in lista cu cartile cu fata in jos
            self.piles[bottom_face_down_pile].append(card)

        for pile_no in range(play_pile_1, play_pile_7 + 1):  # aranjeaza cartile
            for j in range(pile_no - play_pile_1 + 1):
                card = self.piles[bottom_face_down_pile].pop()
                self.piles[pile_no].append(card)
                card.position = self.pile_mat_list[pile_no].position
                self.pull_to_top(card)

        for i in range(play_pile_1, play_pile_7 + 1):  # intoarce cu fata in sus cartile aranjate
            self.piles[i][-1].face_up()

    def on_draw(self):

        arcade.start_render()

        self.pile_mat_list.draw()

        self.card_list.draw()
        if len(self.piles[top_pile_1]) == 13 and len(self.piles[top_pile_2]) == 13 and len(self.piles[top_pile_3]) == \
                13 and len(self.piles[top_pile_4]) == 13:
            arcade.draw_text("BRAVO, AI CASTIGAT!",
                             200, 300, arcade.color.BLACK, 50)

    def pull_to_top(self, card):
        index = self.card_list.index(card)
        for i in range(index, len(self.card_list) - 1):  # ia toate celelalte carti de jos pana la sfarsit
            self.card_list[i] = self.card_list[i + 1]
        self.card_list[len(self.card_list) - 1] = card  # pune cartea asta ultima

    def on_key_press(self, symbol: int, modifiers: int):
        if symbol == arcade.key.R:
            self.setup()

    def on_mouse_press(self, x: float, y: float, button: int, modifiers: int):
        """se apeleaza mereu cand apasam"""
        cards = arcade.get_sprites_at_point((x, y), self.card_list)  # ne de lista cu cartile apasate

        if len(cards) > 0:  # daca am apasat pe o carte

            primary_card = cards[-1]  # ia prima carte

            pile_index = self.get_pile_for_card(primary_card)  # in ce pachet se afla cartea

            if pile_index == bottom_face_down_pile:  # daca apasam pe pachetul de jos
                for i in range(1):  # da 1 carti
                    if len(self.piles[bottom_face_down_pile]) == 0:  # daca am ramas fara carti
                        break
                    card = self.piles[bottom_face_down_pile][-1]  # ia cartea de deasupra
                    card.face_up()  # intoarce cartea
                    card.position = self.pile_mat_list[bottom_face_up_pile].position  # muta cartea in partea dreapta
                    self.piles[bottom_face_down_pile].remove(card)  # sterge cartea din stanga
                    self.piles[bottom_face_up_pile].append(card)  # baga cardu in lista de carti cu fata in sus
                    self.pull_to_top(card)
            elif primary_card.is_face_down:  # daca e cu fata in jos si e din cele 7 pachete
                primary_card.face_up()
            else:  # celelalte cazuri ia cartea
                self.held_cards = [primary_card]
                self.held_cards_original_position = [self.held_cards[0].position]  # salveaza poz
                self.pull_to_top(self.held_cards[0])

                card_index = self.piles[pile_index].index(primary_card)  # daca sunt mai multe carti luate
                for i in range(card_index + 1, len(self.piles[pile_index])):
                    card = self.piles[pile_index][i]
                    self.held_cards.append(card)
                    self.held_cards_original_position.append(card.position)
                    self.pull_to_top(card)
        else:  # am apasat pe un mat
            mats = arcade.get_sprites_at_point((x, y), self.pile_mat_list)

            if len(mats) > 0:
                mat = mats[0]
                mat_index = self.pile_mat_list.index(mat)

                # daca am apasat pe mat-ul de jos si nu mai are carti
                if mat_index == bottom_face_down_pile and len(self.piles[bottom_face_down_pile]) == 0:
                    temp_list = self.piles[bottom_face_up_pile].copy()
                    for card in reversed(temp_list):
                        card.face_down()
                        self.piles[bottom_face_up_pile].remove(card)
                        self.piles[bottom_face_down_pile].append(card)
                        card.position = self.pile_mat_list[bottom_face_down_pile].position

    def get_pile_for_card(self, card):
        for index, pile in enumerate(self.piles):
            if card in pile:
                return index

    def remove_card_from_pile(self, card):
        for pile in self.piles:
            if card in pile:
                pile.remove(card)
                break

    def move_card_to_new_pile(self, card, pile_index):
        self.remove_card_from_pile(card)
        self.piles[pile_index].append(card)

    def on_mouse_release(self, x: float, y: float, button: int,
                         modifiers: int):
        """se apeleaza cand apasam"""
        if len(self.held_cards) == 0:  # daca nu avem nicio carte nu fa nimic
            return

        # gaseste cel mai apropiat pachet
        pile, distance = arcade.get_closest_sprite(self.held_cards[0], self.pile_mat_list)
        reset_position = True

        if arcade.check_for_collision(self.held_cards[0], pile):  # daca atinge un pachet

            pile_index = self.pile_mat_list.index(pile)  # in ce pachet

            if pile_index == self.get_pile_for_card(self.held_cards[0]):
                pass  # daca punem peste acelasi pachet din care am luat
            elif play_pile_1 <= pile_index <= play_pile_7:  # daca e pachet de play
                if len(self.piles[pile_index]) > 0:
                    if self.held_cards[0].numar + 1 == self.piles[pile_index][-1].numar:
                        if ((self.held_cards[0].suit == "Spades" or self.held_cards[0].suit == "Clubs") and
                            (self.piles[pile_index][-1].suit == "Hearts" or self.piles[pile_index][-1].suit ==
                             "Diamonds")) or ((self.held_cards[0].suit == "Hearts" or self.held_cards[0].suit ==
                                               "Diamonds") and (self.piles[pile_index][-1].suit == "Spades" or
                                                                self.piles[pile_index][-1].suit == "Clubs")):
                            if len(self.piles[pile_index]) > 0:
                                top_card = self.piles[pile_index][-1]
                                for i, dropped_card in enumerate(self.held_cards):
                                    dropped_card.position = top_card.center_x, \
                                                            top_card.center_y - card_vertical_offset * (i + 1)
                            else:  # nu-s carti in pachetul care punem
                                for i, dropped_card in enumerate(self.held_cards):
                                    dropped_card.position = pile.center_x, \
                                                        pile.center_y - card_vertical_offset * i
                            for card in self.held_cards:
                                self.move_card_to_new_pile(card, pile_index)

                            reset_position = False
                elif self.held_cards[0].numar == 13:
                    for i, dropped_card in enumerate(self.held_cards):
                        dropped_card.position = pile.center_x, \
                                                pile.center_y - card_vertical_offset * i
                    for card in self.held_cards:
                        self.move_card_to_new_pile(card, pile_index)
                    reset_position = False

            elif top_pile_1 <= pile_index <= top_pile_4 and len(self.held_cards) == 1:
                self.held_cards[0].position = pile.position
                if len(self.piles[pile_index]) == 0 and self.held_cards[0].numar == 1:
                    for card in self.held_cards:
                        self.move_card_to_new_pile(card, pile_index)
                    reset_position = False
                elif len(self.piles[pile_index]) > 0 and self.held_cards[0].numar - 1 == \
                        self.piles[pile_index][-1].numar and self.held_cards[0].suit == self.piles[pile_index][-1].suit:
                    for card in self.held_cards:
                        self.move_card_to_new_pile(card, pile_index)
                    reset_position = False

        if reset_position:
            for pile_index, card in enumerate(self.held_cards):
                card.position = self.held_cards_original_position[pile_index]

        self.held_cards = []

    def on_mouse_motion(self, x: float, y: float, dx: float, dy: float):
        """cand miscam mouse"""
        for card in self.held_cards:
            card.center_x += dx
            card.center_y += dy


def main():
    window = MyGame()
    window.setup()
    arcade.run()


if __name__ == "__main__":
    main()
