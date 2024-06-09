// src/app/enums.ts

export enum Module {
    Dashboard = 1,
    QlDm = 2,
    QlPr = 3,
    QlHdn = 4,
    QlHdb = 5,
    QlDh = 6,
    QlG = 7,
    QlKh = 8,
    QlNv = 9,
    QlNcc = 10
}

export enum Type {
    Read = 1,
    Create = 2,
    Update = 3,
    Deleted = 4
}

export const moduleTypeMap: { [key in Module]: Type[] } = {
    [Module.Dashboard]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlDm]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlPr]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlHdn]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlHdb]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlDh]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlG]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlKh]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlNv]: [Type.Read, Type.Create, Type.Update, Type.Deleted],
    [Module.QlNcc]: [Type.Read, Type.Create, Type.Update, Type.Deleted]

};
