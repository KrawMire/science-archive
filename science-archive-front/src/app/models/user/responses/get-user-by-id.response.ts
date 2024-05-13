import { User } from "@models/user/user";

export interface GetUserByIdResponse {
  /**
   * Requested user
   */
  user: User;
}
