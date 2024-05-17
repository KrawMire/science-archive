import { User } from "@models/user/user";

/**
 * Represents response to get all users from API
 */
export interface GetAllUsersResponse {
  /**
   * All existing users
   */
  users: User[];
}
