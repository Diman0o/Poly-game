def is_level_valid(levelInfo: str):
    print(levelInfo)
    lines = levelInfo.split("\n")
    return is_header_valid(lines[0]) and is_body_valid(lines[1:])
    

def is_header_valid(header: str):
    for camera_rotation in header.split(","):
        if not is_float(camera_rotation):
            print("HEADER INVALID")
            return False
    return True
    

def is_body_valid(triangles: [str]):
    for triangle in triangles:
        if not is_triangle_valid(triangle):
            print("BODY INVALID")
            return False
    return True


def is_triangle_valid(triangle: str):
    triangle_components = triangle.split(",")
    #Triangle components are:
    #Ax, Ay, Az, Bx, By, Bz, Cx, Cy, Cz, ColorR, ColorG, ColorB, aMoveCoef, bMoveCoef, cMoveCoef
    if len(triangle_components) != 15:
        return False

    if not are_triangle_coolrdinates_valid(triangle_components[:9]):
        print("TRIANGLE COORDINATES INVALID")
        return False
    if not are_colors_valid(triangle_components[9:12]):
        print("TRIANGLE COLORS INVALID")
        return False
    if not are_move_coefs_valid(triangle_components[12:16]):
        print("TRIANGLE MOVE COEFS INVALID")
        return False
    return True
        

def are_triangle_coolrdinates_valid(coordinates: [str]):
    print(f"coordinates: {coordinates}")
    for coordinate in coordinates:
        if not is_float(coordinate):
            print(f"{coordinate} is not float")
            return False
        float_coordinate = float(coordinate)
        if -30 > float_coordinate or float_coordinate > 30:
            print(f"{coordinate} not between -30..30")
            return False
    return True


def are_colors_valid(colors: [str]):
    for color in colors:
        if not is_float(color):
            return False
        float_color = float(color)
        if 0 > float_color or float_color > 1:
            print(f"{float_color} not between -0..1")
            return False
    return True


def are_move_coefs_valid(move_coefs: [str]):
    for move_coef in move_coefs:
        if not is_float(move_coef):
            return False
        float_move_coef = float(move_coef)
        if float_move_coef < -2 or float_move_coef > 2:
            return False
    return True
    
def is_float(string: str):
    try:
        float(string)
        return True
    except:
        return False