import uuid
import hashlib
from fastapi import FastAPI, Depends
from sqlalchemy.orm import Session

from .database import get_db
from .models import Auth, Account, LevelPass, Level
from .level import is_level_valid

salt = "h82dxyuf34hcd8yfh93yhcfiug34cygv3bkjf"

app = FastAPI()


@app.get("/")
async def root():
    return {"message": "Hello World"}


@app.get("/register/")
def register(login: str = "", password: str = "", db: Session = Depends(get_db)):
    if login == "" or password == "":
        return "ERROR"
    hash = hashlib.sha256((salt + password).encode("utf-8")).hexdigest()
    innerId = str(uuid.uuid4())
    auth = Auth(login=login, hash=hash, innerId=innerId)
    account = Account(id=innerId)
    db.add(auth)
    db.add(account)
    try:
        db.commit()
        return "OK"
    except: #probably login is occupied
        return "ERROR"
    
    
@app.get("/login/")
def login(login: str = "", password: str = "", db: Session = Depends(get_db)):
    auth = db.query(Auth).filter(Auth.login == login).first()
    hash = hashlib.sha256((salt + password).encode('utf-8')).hexdigest()
    if hash == auth.hash:
        return auth.login + "," + auth.innerId
    else:
        return "ERROR"


@app.get("/pass_level/")
def pass_level(accountId: str = "", levelName = "", db: Session = Depends(get_db)):
    if "," in accountId or ";" in accountId:
        return "ERROR"
    levelPass = LevelPass(accountId=accountId, levelName=levelName)
    db.add(levelPass)
    try:
        db.commit()
        return "OK"
    except: #probably accountId is incorrect
        return "ERROR"
    
    
@app.get("/get_passed_levels")
def get_passed_levels(accountId: str = "", db: Session = Depends(get_db)):
    dbEntries = db.query(LevelPass).filter(LevelPass.accountId == accountId).all()
    levelNames = map(lambda dbEntry: dbEntry.levelName, dbEntries)
    print(levelNames)
    return ",".join(levelNames)


@app.get("/add_level")
def add_level(accountId: str = "", levelData: str = "", db: Session = Depends(get_db)):
    if accountId == "":
        return "ERROR:NOT_AUTHED"
    if not is_level_valid(levelData):
        return "ERROR:LEVEL_INVALID"
    level = Level(authorId=accountId, data=levelData)
    db.add(level)
    try:
        db.commit()
        return "OK"
    except:
        return "ERROR:UNDEFINED_DB_ERROR"
    
    
@app.get("/get_levels")
def get_levels(db: Session = Depends(get_db)):
    response = []
    for (level, auth) in db.query(Level, Auth).filter(Level.authorId == Auth.innerId).all():
        response.append(str(level.id) + ";" + auth.login)
    return ",".join(response)


@app.get("/get_level_info")
def get_level_info(levelName: str, db: Session = Depends(get_db)):
    level = db.query(Level).filter(Level.id == levelName).first()
    return level.data


@app.get('/debug/')
def debug(table: str = "", db: Session = Depends(get_db)):
    if table == "auth":
        return db.query(Auth).all()
    if table == "account":
        return db.query(Account).all()
    if table == "level_pass":
        return db.query(LevelPass).all()
    return "WRONG TABLE NAME"
