# build a schema using pydantic
from pydantic import BaseModel

class Auth(BaseModel):
    login: str
    hash: int
    innerId: int

    class Config:
        orm_mode = True
        

class Account(BaseModel):
    id: str

    class Config:
        orm_mode = True
        
        
class LevelPass(BaseModel):
    id: int
    accountId: str
    levelName: str
    
    class Config:
        orm_mode = True
        

class Level(BaseModel):
    id: int
    authorId: str
    data: str
    
    class Config:
        orm_mode = True

        