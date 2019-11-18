/* 建表时字符编码选择utf8mb4 */

/* 用户表 */
CREATE TABLE USER
(
USER_ID INT PRIMARY KEY,
USER_NAME VARCHAR(16) NOT NULL,
NICKNAME VARCHAR(32) NOT NULL,
PASSWORD VARCHAR(128) NOT NULL,
PHONE VARCHAR(32),
BIRTHDATE VARCHAR(16),
SEX VARCHAR(4),
ADDRESS VARCHAR(128)
);

/* 商品表 */
CREATE TABLE GOODS
(
GOODS_ID INT PRIMARY KEY,
USER_ID INT NOT NULL,
UP_TIME VARCHAR(32) NOT NULL,
EDIT_TIME VARCHAR(32) NOT NULL,
TITLE VARCHAR(32) NOT NULL,
DESCRIPTION VARCHAR(1000) NOT NULL,
TAGS VARCHAR(64),
STATUS VARCHAR(4) NOT NULL,
PRICE NUMERIC(14,2) NOT NULL,
ADDRESS VARCHAR(128) NOT NULL,
FOREIGN KEY (USER_ID) REFERENCES USER(USER_ID)
);

/* 评论表 */
CREATE TABLE COMMENT
(
COMMENT_ID VARCHAR(128) PRIMARY KEY,
GOODS_ID INT NOT NULL,
UP_TIME VARCHAR(32) NOT NULL,
COMMENT VARCHAR(256) NOT NULL,
USER_ID INT NOT NULL,
FOREIGN KEY (GOODS_ID) REFERENCES GOODS(GOODS_ID),
FOREIGN KEY (USER_ID) REFERENCES USER(USER_ID)
);

/* 添加第一个用户 */
INSERT INTO USER (USER_ID,USER_NAME,NICKNAME,PASSWORD)
VALUES(1,'zzz','Z19杂货铺',/*'z19'*/'9c2b72aaff10c09a072b1a16d44251fc');