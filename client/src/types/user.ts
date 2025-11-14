export type User = {
    id: string;
    displayName: string;
    mail: string;
    token: string;
    imageUrl?: string;
}

export type LoginCreds = {
    mail: string;
    password: string;
}

export type RegisterCreds = {
    mail: string;
    displayName: string;
    password: string;
}