
/**
 * @classdesc Enum that contains supported <code>operation</code>'s type.
 * @enum LvfOperationType
 */
export enum LvfOperationType {

    NONE = 0,
    CREATE = 1,
    READ = 2,
    UPDATE = 3,
    UPDATE_STATUS = 4,
    UPDATE_CONFIG = 5,
    DELETE = 6,
    READ_ALL = 7,
    VALIDATION = 20,
    SEARCH = 30,
    PRINTING = 40,
    UPDLOADING = 50,
    DOWNLOAD = 51,
    SENDING_BY_EMAIL = 60,
    COMMON = 100,
    LOG_IN = 110,
    LOG_OUT = 111
}
