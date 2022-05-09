from sqlalchemy import Column, ForeignKey, Integer, String
from .database import Base

class Auth(Base):
    __tablename__ = 'auth'
    login = Column(String, primary_key=True, index=True)
    hash = Column(String)
    innerId = Column(String, ForeignKey("account.id"))
    #author_id = Column(Integer, ForeignKey('author.id'))
    #author = relationship('Author')

class Account(Base):
    __tablename__ = 'account'
    id = Column(String, primary_key=True, index=True)
    

class LevelPass(Base):
    __tablename__ = 'level_pass'
    id = Column(Integer, primary_key=True, index=True)
    accountId = Column(String, ForeignKey("account.id"), index=True)
    levelName = Column(String)
