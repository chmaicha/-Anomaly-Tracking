import { IUserInfo, User } from "src/app/views/anomalytracking.domain/_shared/models/user/user";


export interface IAuthenticationData {

    User: IUserInfo;
}

export class AuthenticationData {

    User: User;

    constructor(info: IAuthenticationData) {
        this.User = info.User ? new User(info.User) : null;
    }
}
