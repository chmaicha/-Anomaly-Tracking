import { EntityBase, IEntityBaseInfo } from "src/app/views/core.domain/shared/models/common/entitybase";
import { LvfUserRole } from "src/app/views/core.domain/shared/enums/lvf-user-role";
/**
 * Interface that represents user's information object.
 */
export interface IUserInfo extends IEntityBaseInfo {

  FirstName: string;
  LastName: string;
  Code: string;
  Password: string;
  LvfUserRole: LvfUserRole;
  LvfUserRoleLabel: string;
}

/**
 * @class User
 * @classdesc Represents a user object.
 */
export class User extends EntityBase {

  FirstName: string;
  LastName: string;
  Code: string;
  Password: string;
  LvfUserRole: LvfUserRole;
  LvfUserRoleLabel: string;


    /**
     * @constructor
     * Creates an instance of user.
     */
    constructor(info: IUserInfo) {
        super(info);

        this.FirstName = info.FirstName;
        this.LastName = info.LastName;
        this.Code = info.Code;
        this.Password = info.Password;
        this.LvfUserRole = info.LvfUserRole;
        this.LvfUserRoleLabel = info.LvfUserRole ? `app.lvfuserrole.${LvfUserRole[this.LvfUserRole].toLowerCase()}` : "";
     }
}

