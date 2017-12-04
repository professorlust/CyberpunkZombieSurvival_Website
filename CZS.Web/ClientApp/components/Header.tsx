﻿import * as React from 'react';
import { Link } from 'react-router-dom';
import * as dotnetify from 'dotnetify';
import Logout from './Logout';

export default class Header extends React.Component<any, any> {
    vm: any;
    dispatchState: any;

    constructor(props: any) {
        super(props);
        this.vm = dotnetify.react.connect('HeaderViewModel', this);
        this.dispatchState = state => this.vm.$dispatch(state);

        this.state = { Username: '', AvatarURL: '' }

        this.confirmLogout = this.confirmLogout.bind(this);
    }

    componentWillUnmount() {
        this.vm.$destroy();
    }
    
    confirmLogout() {
        window.location.replace('/Authorization/Logout');
    }

    render() {
        return (
            <div>
                <Logout callback={this.confirmLogout} />

                <nav className="navbar navbar-expand-lg navbar-dark bg-primary">

                    <Link className="navbar-brand" to="/">
                        Cyberpunk Zombie Survival <br />
                    </Link>

                    <div className="navbar">
                        <ul className="navbar-nav">
                            <li className="nav-item">
                                <Link className="nav-link" to="/features">
                                    <i className="fa fa-info-circle fa-lg" /> Features
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="/downloads">
                                    <i className="fa fa-download fa-lg" /> Downloads
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="https://discord.gg/sg45eY8" target="_blank">
                                    <i className="fa fa-commenting-o fa-lg" /> Discord (Chat)
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="http://czs.wikia.com/" target="_blank">
                                    <i className="fa fa-wikipedia-w fa-lg" /> Wiki
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="http://czs.boards.net/" target="_blank">
                                    <i className="fa fa-th-list fa-lg" /> Forums
                                </Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to="https://github.com/zunath/CyberpunkZombieSurvival_JVM" target="_blank">
                                    <i className="fa fa-code fa-lg" /> Source Code
                                </Link>
                            </li>

                            {this.state.Username === '' || this.state.Username === null || this.state.Username === undefined ?

                                <li className="nav-item pull-right">
                                    <a className="nav-link" href="/Authorization/Login">
                                        <i className="fa fa-sign-in fa-lg" /> Login (With Discord)
                                    </a>
                                </li> :
                                <li className="nav-item dropdown">
                                    <Link id="userDropdown" className="nav-link dropdown-toggle" to="#" data-toggle="dropdown" role="button">
                                        <i className="fa fa-user fa-lg" /> {this.state.Username}
                                    </Link>
                                    <div className="dropdown-menu">
                                        <Link className="dropdown-item" to="#">
                                            <i className="fa fa-user" /> Profile
                                        </Link>
                                        <Link className="dropdown-item" to="#" data-toggle="modal" data-target="#logoutModal">
                                            <i className="fa fa-sign-out" /> Log Out
                                        </Link>
                                    </div>
                                </li>
                            }
                        </ul>
                    </div>


                </nav>
            </div>
        );



    }
}
