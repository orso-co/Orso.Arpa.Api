(self.webpackChunkorso_arpa_web=self.webpackChunkorso_arpa_web||[]).push([[369],{369:(t,e,i)=>{"use strict";i.d(e,{o:()=>s});var o=i(440),r=i(8619),a=i(7688);let s=(()=>{class t{constructor(t){this.projectService=t}resolve(t){return t.component===o.X?this.projectService.load(!0):this.projectService.load()}}return t.\u0275fac=function(e){return new(e||t)(r.LFG(a.Y))},t.\u0275prov=r.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})()},5166:(t,e,i)=>{"use strict";i.d(e,{w:()=>s});var o=i(8303),r=i(8619),a=i(8611);let s=(()=>{class t{constructor(t){this.apiService=t,this.baseUrl="/me"}getMyProfile(){return this.apiService.get(`${this.baseUrl}/profiles/user`).pipe((0,o.d)())}putProfile(t){return this.apiService.put(`${this.baseUrl}/profiles/user`,t).pipe((0,o.d)())}getMyAppointments(t,e){return this.apiService.get(`${this.baseUrl}/appointments?limit=${t}&offset=${e}`).pipe((0,o.d)())}setAppointmentPrediction(t,e){return this.apiService.put(`${this.baseUrl}/appointments/${t}/participation/prediction/${e}`,{}).pipe((0,o.d)())}getProfileMusician(t){return t?this.apiService.get(`${this.baseUrl}/profiles/musician/${t}`).pipe((0,o.d)()):this.apiService.get(`${this.baseUrl}/profiles/musician`).pipe((0,o.d)())}putProfileMusician(t){return this.apiService.post(`${this.baseUrl}/profiles/musician`,t).pipe((0,o.d)())}putProjectParticipation(t,e,i){return this.apiService.put(`${this.baseUrl}/profiles/musician/${t}/projects/${e}/participation`,i).pipe((0,o.d)())}}return t.\u0275fac=function(e){return new(e||t)(r.LFG(a.s))},t.\u0275prov=r.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})()},7688:(t,e,i)=>{"use strict";i.d(e,{Y:()=>n});var o=i(8303),r=i(2693),a=i(8619),s=i(8611);let n=(()=>{class t{constructor(t){this.apiService=t,this.baseUrl="/projects"}load(t){if(t){const e=(new r.LE).set("includeCompleted",t.toString());return this.apiService.get(this.baseUrl,e).pipe((0,o.d)())}return this.apiService.get(this.baseUrl).pipe((0,o.d)())}create(t){return this.apiService.post(this.baseUrl,t)}update(t){return this.apiService.put(`${this.baseUrl}/${t.id}`,t).pipe((0,o.d)())}getParticipations(t){return this.apiService.get(`${this.baseUrl}/${t}/participations`).pipe((0,o.d)())}}return t.\u0275fac=function(e){return new(e||t)(a.LFG(s.s))},t.\u0275prov=a.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})()},885:(t,e,i)=>{"use strict";i.d(e,{l:()=>l});var o=i(8512),r=i(2693),a=i(8303),s=i(6599),n=i(8619),p=i(8611);let l=(()=>{class t{constructor(t){this.apiService=t,this.sectionTrees=new Map,this.sections$$=new o.X([]),this.sections$=this.sections$$.asObservable(),this.sectionsLoaded=!1,this.baseUrl="/sections"}load(){return this.apiService.get(this.baseUrl).pipe((0,a.d)(),(0,s.b)(t=>this.sections$$.next(t)),(0,s.b)(t=>this.sectionsLoaded=!0))}loadTree(t){let e=new r.LE;return t&&(e=e.set("maxLevel",t.toString())),this.apiService.get(`${this.baseUrl}/tree`,e).pipe((0,a.d)(),(0,s.b)(e=>this.sectionTrees.set(t,e)))}treeLoaded(t){return this.sectionTrees.has(t)}getTree(t){return this.sectionTrees.get(t)}}return t.\u0275fac=function(e){return new(e||t)(n.LFG(p.s))},t.\u0275prov=n.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})()},7558:(t,e,i)=>{"use strict";i.d(e,{W:()=>p});var o=i(8303),r=i(9996),a=i(6599),s=i(8619),n=i(8611);let p=(()=>{class t{constructor(t){this.apiService=t,this.selectValues=new Map,this.baseUrl="/tables"}load(t,e){return this.apiService.get(`${this.baseUrl}/${t}/properties/${e}`).pipe((0,o.d)(),(0,r.U)(t=>t.map(t=>this.mapSelectValueToSelectItem(t))),(0,a.b)(i=>this.selectValues.set(this.getMapKey(t,e),i)))}loaded(t,e){return this.selectValues.has(this.getMapKey(t,e))}get(t,e){return this.selectValues.get(this.getMapKey(t,e))||[]}getMapKey(t,e){return`${t}|${e}`}mapSelectValueToSelectItem(t){return{label:t.name,value:t.id}}}return t.\u0275fac=function(e){return new(e||t)(s.LFG(n.s))},t.\u0275prov=s.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})()},6294:(t,e,i)=>{"use strict";i.d(e,{P:()=>s});var o=i(8303),r=i(8619),a=i(8611);let s=(()=>{class t{constructor(t){this.apiService=t,this.baseUrl="/venues"}load(){return this.apiService.get(this.baseUrl).pipe((0,o.d)())}loadRooms(t){return this.apiService.get(`${this.baseUrl}/${t}/rooms`).pipe((0,o.d)())}resolve(t){return this.load()}}return t.\u0275fac=function(e){return new(e||t)(r.LFG(a.s))},t.\u0275prov=r.Yz7({token:t,factory:t.\u0275fac,providedIn:"root"}),t})()},440:(t,e,i)=>{"use strict";i.d(e,{X:()=>G});var o=i(4762),r=i(9996),a=i(3530),s=i(5932),n=i(1041),p=i(8619),l=i(46),c=i(649),u=i(4313),d=i(6326),Z=i(9366),h=i(4217),g=i(1116);const f=function(){return[]};let m=(()=>{class t{constructor(t,e,i,o){this.config=t,this.formBuilder=e,this.ref=i,this.translate=o,this.project=this.config.data.project,this.venues=this.config.data.venues.pipe((0,r.U)(t=>t.map(t=>({label:this.getAddress(t),value:t.id})))),this.type=this.config.data.type,this.genre=this.config.data.genre,this.state=this.config.data.state,this.completedOptions=[{label:this.translate.instant("YES"),value:!0},{label:this.translate.instant("NO"),value:!1}],this.parentProject=this.config.data.projects.pipe((0,r.U)(t=>t.filter(t=>t!==this.project).map(t=>({label:t.title,value:t.id}))))}get isNew(){return!this.project}getAddress(t){if(t.address){const{city:e,urbanDistrict:i}=t.address,o=`${e||""}${e&&i?" ":""}${i||""}`;return`${o}${o?" | ":""}${t.name}`}return t.name}ngOnInit(){this.form=this.formBuilder.group({title:[null,[n.kI.required]],shortTitle:[null,[n.kI.required]],startDate:[null],endDate:[null],description:[null],typeId:[null],stateId:[null],genreId:[null],parentId:[null],code:[null,[n.kI.required]],isCompleted:[null,[n.kI.required]]}),this.isNew||this.form.patchValue(Object.assign(Object.assign({},this.project),{startDate:new Date(this.project.startDate),endDate:new Date(this.project.endDate)}))}onSubmit(){this.form.invalid||this.form.pristine||this.ref.close(Object.assign(Object.assign({},this.project),this.form.value))}cancel(){this.ref.close(null)}}return t.\u0275fac=function(e){return new(e||t)(p.Y36(l.S),p.Y36(n.qu),p.Y36(l.E7),p.Y36(c.sK))},t.\u0275cmp=p.Xpm({type:t,selectors:[["arpa-edit-project"]],decls:77,vars:80,consts:[[3,"formGroup"],[1,"p-fluid","p-formgrid","p-grid"],[1,"p-col-12","p-md-4"],[1,"p-field","p-col-12"],["for","title"],["type","text","pInputText","","formControlName","title","maxlength","50","id","title"],["for","startDate"],["formControlName","startDate","id","startDate","dateFormat","dd MM yy",3,"showWeek"],["for","endDate"],["formControlName","endDate","id","endDate","dateFormat","dd MM yy",3,"showWeek"],["for","isCompleted"],["appendTo","body","formControlName","isCompleted","id","isCompleted",3,"showClear","required","autoDisplayFirst","options"],["for","shortTitle"],["type","text","pInputText","","formControlName","shortTitle","maxlength","50","id","shortTitle"],["for","genre"],["appendTo","body","formControlName","genreId","id","genre",3,"showClear","autoDisplayFirst","options"],["for","type"],["appendTo","body","formControlName","typeId","id","type",3,"showClear","autoDisplayFirst","options"],["for","venue"],["appendTo","body","id","venue","name","venue",3,"autoDisplayFirst","showClear","options"],["for","code"],["type","text","pInputText","","formControlName","code","maxlength","15","id","code"],["for","parent"],["appendTo","body","formControlName","parentId","id","parent",3,"showClear","autoDisplayFirst","options"],["for","stateId"],["appendTo","body","formControlName","stateId","id","state",3,"showClear","autoDisplayFirst","options"],["for","description"],["formControlName","description","id","description"],[1,"p-formgroup-inline","p-jc-between"],[1,"p-field","p-col"],["pButton","","type","button","icon","pi pi-times","iconPos","left",1,"p-ml-3","p-button-danger",3,"label","click"],[1,"p-field","p-col","p-text-right",2,"margin-right","0"],["pButton","","type","submit","icon","pi pi-check","iconPos","left",3,"label","disabled","click"]],template:function(t,e){1&t&&(p.TgZ(0,"form",0),p.TgZ(1,"div",1),p.TgZ(2,"div",2),p.TgZ(3,"div",3),p.TgZ(4,"label",4),p._uU(5),p.ALo(6,"translate"),p.qZA(),p._UZ(7,"input",5),p.qZA(),p.TgZ(8,"div",3),p.TgZ(9,"label",6),p._uU(10),p.ALo(11,"translate"),p.qZA(),p._UZ(12,"p-calendar",7),p.qZA(),p.TgZ(13,"div",3),p.TgZ(14,"label",8),p._uU(15),p.ALo(16,"translate"),p.qZA(),p._UZ(17,"p-calendar",9),p.qZA(),p.TgZ(18,"div",3),p.TgZ(19,"label",10),p._uU(20),p.ALo(21,"translate"),p.qZA(),p._UZ(22,"p-dropdown",11),p.qZA(),p.qZA(),p.TgZ(23,"div",2),p.TgZ(24,"div",3),p.TgZ(25,"label",12),p._uU(26),p.ALo(27,"translate"),p.qZA(),p._UZ(28,"input",13),p.qZA(),p.TgZ(29,"div",3),p.TgZ(30,"label",14),p._uU(31),p.ALo(32,"translate"),p.qZA(),p._UZ(33,"p-dropdown",15),p.ALo(34,"async"),p.qZA(),p.TgZ(35,"div",3),p.TgZ(36,"label",16),p._uU(37),p.ALo(38,"translate"),p.qZA(),p._UZ(39,"p-dropdown",17),p.ALo(40,"async"),p.qZA(),p.TgZ(41,"div",3),p.TgZ(42,"label",18),p._uU(43),p.ALo(44,"translate"),p.qZA(),p._UZ(45,"p-dropdown",19),p.ALo(46,"async"),p.qZA(),p.qZA(),p.TgZ(47,"div",2),p.TgZ(48,"div",3),p.TgZ(49,"label",20),p._uU(50),p.ALo(51,"translate"),p.qZA(),p._UZ(52,"input",21),p.qZA(),p.TgZ(53,"div",3),p.TgZ(54,"label",22),p._uU(55),p.ALo(56,"translate"),p.qZA(),p._UZ(57,"p-dropdown",23),p.ALo(58,"async"),p.qZA(),p.TgZ(59,"div",3),p.TgZ(60,"label",24),p._uU(61),p.ALo(62,"translate"),p.qZA(),p._UZ(63,"p-dropdown",25),p.ALo(64,"async"),p.qZA(),p.TgZ(65,"div",3),p.TgZ(66,"label",26),p._uU(67),p.ALo(68,"translate"),p.qZA(),p._UZ(69,"textarea",27),p.qZA(),p.qZA(),p.qZA(),p.TgZ(70,"div",28),p.TgZ(71,"div",29),p.TgZ(72,"button",30),p.NdJ("click",function(){return e.cancel()}),p.ALo(73,"translate"),p.qZA(),p.qZA(),p.TgZ(74,"div",31),p.TgZ(75,"button",32),p.NdJ("click",function(){return e.onSubmit()}),p.ALo(76,"translate"),p.qZA(),p.qZA(),p.qZA(),p.qZA()),2&t&&(p.Q6J("formGroup",e.form),p.xp6(5),p.hij("",p.lcZ(6,37,"projects.TITLE")," *"),p.xp6(5),p.Oqu(p.lcZ(11,39,"projects.START")),p.xp6(2),p.Q6J("showWeek",!0),p.xp6(3),p.Oqu(p.lcZ(16,41,"projects.END")),p.xp6(2),p.Q6J("showWeek",!0),p.xp6(3),p.hij("",p.lcZ(21,43,"projects.COMPLETED")," *"),p.xp6(2),p.Q6J("showClear",!0)("required",!0)("autoDisplayFirst",!1)("options",e.completedOptions),p.xp6(4),p.hij("",p.lcZ(27,45,"projects.ABBREVIATION")," *"),p.xp6(5),p.Oqu(p.lcZ(32,47,"projects.GENRE")),p.xp6(2),p.Q6J("showClear",!0)("autoDisplayFirst",!1)("options",p.lcZ(34,49,e.genre)||p.DdM(75,f)),p.xp6(4),p.Oqu(p.lcZ(38,51,"projects.TYPE")),p.xp6(2),p.Q6J("showClear",!0)("autoDisplayFirst",!1)("options",p.lcZ(40,53,e.type)||p.DdM(76,f)),p.xp6(4),p.Oqu(p.lcZ(44,55,"editappointments.VENUE")),p.xp6(2),p.Q6J("autoDisplayFirst",!1)("showClear",!0)("options",p.lcZ(46,57,e.venues)||p.DdM(77,f)),p.xp6(5),p.hij("",p.lcZ(51,59,"projects.CODE")," *"),p.xp6(5),p.Oqu(p.lcZ(56,61,"projects.PARENT")),p.xp6(2),p.Q6J("showClear",!0)("autoDisplayFirst",!1)("options",p.lcZ(58,63,e.parentProject)||p.DdM(78,f)),p.xp6(4),p.Oqu(p.lcZ(62,65,"projects.STATE")),p.xp6(2),p.Q6J("showClear",!0)("autoDisplayFirst",!1)("options",p.lcZ(64,67,e.state)||p.DdM(79,f)),p.xp6(4),p.Oqu(p.lcZ(68,69,"projects.DESCRIPTION")),p.xp6(5),p.Q6J("label",p.lcZ(73,71,"CANCEL")),p.xp6(3),p.Q6J("label",p.lcZ(76,73,"editappointments.SAVECLOSE"))("disabled",e.form.pristine||e.form.invalid))},directives:[n._Y,n.JL,n.sg,n.Fj,u.o,n.JJ,n.u,n.nD,d.f,Z.Lt,n.Q7,h.Hq],pipes:[c.X$,g.Ov],styles:[""]}),t})();var A=i(436),T=i(5590);const b=function(){return[]};let v=(()=>{class t{constructor(t,e,i){this.config=t,this.formBuilder=e,this.ref=i,this.musicianProfiles=this.config.data.musicianProfiles.pipe((0,A.b)(t=>this.sections.pipe((0,r.U)(e=>t.map(t=>({label:e.find(e=>e.id===t.instrumentId).name,value:t.id})))))),this.sections=this.config.data.sections,this.projectParticipation=this.config.data.projectParticipation}ngOnInit(){this.form=this.formBuilder.group({musicianId:[null,[n.kI.required]],statusId:[null,[n.kI.required]],comment:[null,[]]})}onSubmit(){this.form.invalid||this.form.pristine||this.ref.close(this.form.value)}cancel(){this.ref.close(null)}}return t.\u0275fac=function(e){return new(e||t)(p.Y36(l.S),p.Y36(n.qu),p.Y36(l.E7))},t.\u0275cmp=p.Xpm({type:t,selectors:[["arpa-project-participation"]],decls:30,vars:32,consts:[[3,"formGroup","submit"],[1,"p-fluid","p-formgrid","p-grid"],[1,"p-field","p-col"],["for","musicianId"],["optionLabel","label","optionValue","value","formControlName","musicianId",3,"options","placeholder"],["for","statusId"],["optionLabel","label","optionValue","value","formControlName","statusId",3,"options","placeholder"],["for","comment"],["pInputTextarea","","formControlName","comment",3,"autoResize"],[1,"p-formgroup-inline","p-jc-between"],["pButton","","type","button","icon","pi pi-times","iconPos","left",1,"p-button-danger",3,"label","click"],[1,"p-field","p-col","p-text-right",2,"margin-right","0"],["pButton","","type","button","icon","pi pi-check","iconPos","left",3,"label","disabled","click"]],template:function(t,e){1&t&&(p.TgZ(0,"form",0),p.NdJ("submit",function(t){return t.preventDefault()}),p.TgZ(1,"div",1),p.TgZ(2,"div",2),p.TgZ(3,"label",3),p._uU(4),p.ALo(5,"translate"),p.qZA(),p._UZ(6,"p-dropdown",4),p.ALo(7,"async"),p.ALo(8,"translate"),p.qZA(),p.qZA(),p.TgZ(9,"div",1),p.TgZ(10,"div",2),p.TgZ(11,"label",5),p._uU(12),p.ALo(13,"translate"),p.qZA(),p._UZ(14,"p-dropdown",6),p.ALo(15,"async"),p.ALo(16,"translate"),p.qZA(),p.qZA(),p.TgZ(17,"div",1),p.TgZ(18,"div",2),p.TgZ(19,"label",7),p._uU(20),p.ALo(21,"translate"),p.qZA(),p._UZ(22,"textarea",8),p.qZA(),p.qZA(),p.TgZ(23,"div",9),p.TgZ(24,"div",2),p.TgZ(25,"button",10),p.NdJ("click",function(){return e.cancel()}),p.ALo(26,"translate"),p.qZA(),p.qZA(),p.TgZ(27,"div",11),p.TgZ(28,"button",12),p.NdJ("click",function(){return e.onSubmit()}),p.ALo(29,"translate"),p.qZA(),p.qZA(),p.qZA(),p.qZA()),2&t&&(p.Q6J("formGroup",e.form),p.xp6(4),p.Oqu(p.lcZ(5,12,"projects.PARTICIPATION_MUPRO")),p.xp6(2),p.Q6J("options",p.lcZ(7,14,e.musicianProfiles)||p.DdM(30,b))("placeholder",p.lcZ(8,16,"NOTHING_SELECTED")),p.xp6(6),p.Oqu(p.lcZ(13,18,"projects.PARTICIPATION_STATUS")),p.xp6(2),p.Q6J("options",p.lcZ(15,20,e.projectParticipation)||p.DdM(31,b))("placeholder",p.lcZ(16,22,"NOTHING_SELECTED")),p.xp6(6),p.Oqu(p.lcZ(21,24,"projects.PARTICIPATION_COMMENT")),p.xp6(2),p.Q6J("autoResize",!0),p.xp6(3),p.Q6J("label",p.lcZ(26,26,"CANCEL")),p.xp6(3),p.Q6J("label",p.lcZ(29,28,"SAVE"))("disabled",e.form.pristine||e.form.invalid))},directives:[n._Y,n.JL,n.sg,Z.Lt,n.JJ,n.u,n.Fj,T.g,h.Hq],pipes:[c.X$,g.Ov],styles:[""]}),t})();var q=i(7688),j=i(5469),U=i(8451),x=i(4451);let y=(()=>{class t{constructor(){this.data={labels:["Zusagen","Absagen","Unsicher","Offen"],datasets:[{data:[93,21,42,180],backgroundColor:["#F4D03F","#C0392B","#36A2EB","#8E44AD"],hoverBackgroundColor:["#F9E79F","#CD6155","#36A2E0","#BB8FCE"]}]},this.options={title:{display:!0,text:"This is only fake data for demo",fontSize:14},legend:{position:"bottom"}}}}return t.\u0275fac=function(e){return new(e||t)},t.\u0275cmp=p.Xpm({type:t,selectors:[["arpa-projectchart-participants"]],decls:1,vars:2,consts:[["type","doughnut","width","20vw","height","20vh",3,"data","options"]],template:function(t,e){1&t&&p._UZ(0,"p-chart",0),2&t&&p.Q6J("data",e.data)("options",e.options)},directives:[x.C],styles:[""]}),t})();function S(t,e){if(1&t){const t=p.EpF();p.TgZ(0,"div"),p._UZ(1,"arpa-projectchart-participants"),p._UZ(2,"br"),p._UZ(3,"hr"),p._UZ(4,"br"),p.qZA(),p.TgZ(5,"div",5),p.TgZ(6,"span",6),p._UZ(7,"i",7),p.TgZ(8,"input",8),p.NdJ("input",function(e){return p.CHM(t),p.oxw(),p.MAs(1).filterGlobal(e.target.value,"contains")}),p.ALo(9,"translate"),p.qZA(),p.qZA(),p.TgZ(10,"button",9),p.NdJ("click",function(){p.CHM(t);const e=p.oxw(),i=p.MAs(1);return e.clear(i)}),p.ALo(11,"translate"),p.qZA(),p.qZA()}2&t&&(p.xp6(8),p.Q6J("placeholder",p.lcZ(9,2,"SEARCH")),p.xp6(2),p.Q6J("label",p.lcZ(11,4,"CLEAR")))}function _(t,e){1&t&&(p.TgZ(0,"tr"),p.TgZ(1,"th",10),p.TgZ(2,"div",11),p._uU(3),p.ALo(4,"translate"),p._UZ(5,"p-columnFilter",12),p.qZA(),p.qZA(),p.TgZ(6,"th",13),p.TgZ(7,"div",11),p._uU(8),p.ALo(9,"translate"),p._UZ(10,"p-columnFilter",14),p.qZA(),p.qZA(),p.qZA()),2&t&&(p.xp6(3),p.hij(" ",p.lcZ(4,2,"projects.PARTICIPANTS")," "),p.xp6(5),p.hij(" ",p.lcZ(9,4,"projects.INSTRUMENT")," "))}function C(t,e){if(1&t&&(p.TgZ(0,"tr"),p.TgZ(1,"td"),p._uU(2),p.qZA(),p.TgZ(3,"td"),p._uU(4),p.qZA(),p.qZA()),2&t){const t=e.$implicit;p.xp6(2),p.hij(" ",t.participant," "),p.xp6(2),p.hij(" ",t.instrument," ")}}const P=function(){return[]},I=function(){return[10,25,50]},L=function(){return["participant","instrument"]};let w=(()=>{class t{constructor(t,e){this.projectService=t,this.config=e,this.participants=this.projectService.getParticipations(this.config.data.project.id).pipe((0,r.U)(t=>t.map(t=>({participant:[t.person.givenName,t.person.surname].join(" "),instrument:t.musicianProfile.instrumentName}))))}clear(t){t.clear()}}return t.\u0275fac=function(e){return new(e||t)(p.Y36(q.Y),p.Y36(l.S))},t.\u0275cmp=p.Xpm({type:t,selectors:[["arpa-project-participants"]],decls:7,vars:10,consts:[["dataKey","id",3,"value","rows","rowsPerPageOptions","paginator","globalFilterFields"],["dt1",""],["pTemplate","caption"],["pTemplate","header"],["pTemplate","body"],[1,"p-d-flex"],[1,"p-input-icon-left","p-mr-auto"],[1,"pi","pi-search"],["pInputText","","type","text",3,"placeholder","input"],["pButton","","icon","pi pi-filter-slash",1,"p-button-outlined",3,"label","click"],["pSortableColumn","participant"],[1,"p-d-flex","p-jc-between","p-ai-center"],["type","text","field","participant","display","menu"],["pSortableColumn","instrument"],["type","text","field","instrument","display","menu"]],template:function(t,e){1&t&&(p.TgZ(0,"p-table",0,1),p.ALo(2,"async"),p._uU(3,"> "),p.YNc(4,S,12,6,"ng-template",2),p.YNc(5,_,11,6,"ng-template",3),p.YNc(6,C,5,2,"ng-template",4),p.qZA()),2&t&&p.Q6J("value",p.lcZ(2,5,e.participants)||p.DdM(7,P))("rows",10)("rowsPerPageOptions",p.DdM(8,I))("paginator",!0)("globalFilterFields",p.DdM(9,L))},directives:[j.iA,U.jx,y,u.o,h.Hq,j.lQ,j.xl],pipes:[g.Ov,c.X$],styles:[""]}),t})();var D=i(3464),E=i(7807),N=i(5166),O=i(7558),M=i(6294),k=i(885);function J(t,e){if(1&t){const t=p.EpF();p.TgZ(0,"div",8),p.TgZ(1,"span",9),p._UZ(2,"i",10),p.TgZ(3,"input",11),p.NdJ("input",function(e){return p.CHM(t),p.oxw(),p.MAs(1).filterGlobal(e.target.value,"contains")}),p.ALo(4,"translate"),p.qZA(),p.qZA(),p.TgZ(5,"button",12),p.NdJ("click",function(){p.CHM(t);const e=p.oxw(),i=p.MAs(1);return e.clear(i)}),p.ALo(6,"translate"),p.qZA(),p.qZA()}2&t&&(p.xp6(3),p.Q6J("placeholder",p.lcZ(4,2,"SEARCH")),p.xp6(2),p.Q6J("label",p.lcZ(6,4,"CLEAR")))}function $(t,e){1&t&&(p.TgZ(0,"tr"),p.TgZ(1,"th",13),p.TgZ(2,"div",14),p._uU(3),p.ALo(4,"translate"),p._UZ(5,"p-columnFilter",15),p.qZA(),p.qZA(),p.TgZ(6,"th",16),p.TgZ(7,"div",14),p._uU(8),p.ALo(9,"translate"),p._UZ(10,"p-columnFilter",17),p.qZA(),p.qZA(),p.TgZ(11,"th",18),p.TgZ(12,"div",14),p._uU(13),p.ALo(14,"translate"),p._UZ(15,"p-columnFilter",19),p.qZA(),p.qZA(),p.TgZ(16,"th",20),p.TgZ(17,"div",14),p._uU(18),p.ALo(19,"translate"),p._UZ(20,"p-columnFilter",21),p.qZA(),p.qZA(),p.TgZ(21,"th",22),p.TgZ(22,"div",14),p._uU(23),p.ALo(24,"translate"),p._UZ(25,"p-columnFilter",23),p.qZA(),p.qZA(),p.TgZ(26,"th",24),p.TgZ(27,"div",14),p._uU(28),p.ALo(29,"translate"),p._UZ(30,"p-columnFilter",25),p.qZA(),p.qZA(),p.TgZ(31,"th",26),p.TgZ(32,"div",14),p._uU(33),p.ALo(34,"translate"),p._UZ(35,"p-columnFilter",27),p.qZA(),p.qZA(),p._UZ(36,"th"),p.qZA()),2&t&&(p.xp6(3),p.hij(" ",p.lcZ(4,7,"projects.TITLE")," "),p.xp6(5),p.hij(" ",p.lcZ(9,9,"projects.ABBREVIATION")," "),p.xp6(5),p.hij(" ",p.lcZ(14,11,"projects.VENUE")," "),p.xp6(5),p.hij(" ",p.lcZ(19,13,"projects.START")," "),p.xp6(5),p.hij(" ",p.lcZ(24,15,"projects.END")," "),p.xp6(5),p.hij(" ",p.lcZ(29,17,"projects.STATE")," "),p.xp6(5),p.hij(" ",p.lcZ(34,19,"projects.COMPLETED")," "))}function F(t,e){1&t&&p._UZ(0,"i",35)}function Y(t,e){if(1&t){const t=p.EpF();p.TgZ(0,"button",36),p.NdJ("click",function(e){p.CHM(t);const i=p.oxw().$implicit;return p.oxw().openParticipationDialog(e,i.id)}),p.ALo(1,"translate"),p.qZA()}2&t&&p.Q6J("label",p.lcZ(1,1,"projects.EDIT_PARTICIPATION"))}function R(t,e){if(1&t){const t=p.EpF();p.TgZ(0,"tr",28),p.NdJ("click",function(){const e=p.CHM(t).$implicit;return p.oxw().openProjectDetailDialog(e)}),p.TgZ(1,"td"),p.TgZ(2,"span",29),p._uU(3),p.ALo(4,"translate"),p.qZA(),p._uU(5),p.qZA(),p.TgZ(6,"td"),p.TgZ(7,"span",29),p._uU(8),p.ALo(9,"translate"),p.qZA(),p._uU(10),p.qZA(),p.TgZ(11,"td"),p.TgZ(12,"span",29),p._uU(13),p.ALo(14,"translate"),p.qZA(),p._uU(15),p.qZA(),p.TgZ(16,"td"),p.TgZ(17,"span",29),p._uU(18),p.ALo(19,"translate"),p.qZA(),p._uU(20),p.ALo(21,"date"),p.qZA(),p.TgZ(22,"td"),p.TgZ(23,"span",29),p._uU(24),p.ALo(25,"translate"),p.qZA(),p._uU(26),p.ALo(27,"date"),p.qZA(),p.TgZ(28,"td"),p.TgZ(29,"span",29),p._uU(30),p.ALo(31,"translate"),p.qZA(),p._uU(32),p.ALo(33,"async"),p.qZA(),p.TgZ(34,"td",30),p.TgZ(35,"span",29),p._uU(36),p.ALo(37,"translate"),p.qZA(),p.YNc(38,F,1,0,"i",31),p.qZA(),p.TgZ(39,"td",32),p.YNc(40,Y,2,3,"button",33),p.TgZ(41,"button",34),p.NdJ("click",function(e){const i=p.CHM(t).$implicit;return p.oxw().openParticipationListDialog(e,i)}),p.ALo(42,"translate"),p.qZA(),p.qZA(),p.qZA()}if(2&t){const t=e.$implicit,i=p.oxw();p.xp6(3),p.Oqu(p.lcZ(4,16,"projects.TITLE")),p.xp6(2),p.hij(" ",t.title," "),p.xp6(3),p.Oqu(p.lcZ(9,18,"projects.ABBREVIATION")),p.xp6(2),p.hij(" ",t.shortTitle," "),p.xp6(3),p.Oqu(p.lcZ(14,20,"projects.VENUE")),p.xp6(2),p.hij(" ",t.venue," "),p.xp6(3),p.Oqu(p.lcZ(19,22,"projects.START")),p.xp6(2),p.hij(" ",p.lcZ(21,24,t.startDate)," "),p.xp6(4),p.Oqu(p.lcZ(25,26,"projects.END")),p.xp6(2),p.hij(" ",p.lcZ(27,28,t.endDate)," "),p.xp6(4),p.Oqu(p.lcZ(31,30,"projects.STATE")),p.xp6(2),p.hij(" ",p.lcZ(33,32,i.getState(t.stateId))," "),p.xp6(4),p.Oqu(p.lcZ(37,34,"projects.COMPLETED")),p.xp6(2),p.Q6J("ngIf",t.isCompleted),p.xp6(2),p.Q6J("ngIf",!t.isCompleted),p.xp6(1),p.Q6J("label",p.lcZ(42,36,"projects.SHOW_PARTICIPANTS"))}}const Q=function(){return[]},B=function(){return[10,25,50]},V=function(){return["title","venue"]};let G=(()=>{let t=class{constructor(t,e,i,o,a,s,n,p,l){this.route=t,this.dialogService=e,this.translate=i,this.projectService=o,this.notificationsService=a,this.meService=s,this.selectValueService=n,this.venueService=p,this.sectionService=l,this.projects=this.route.data.pipe((0,r.U)(t=>t.projects)),this.state=this.selectValueService.load("Project","State").pipe((0,r.U)(()=>this.selectValueService.get("Project","State")))}getState(t){return this.state.pipe((0,r.U)(e=>{const i=e.find(e=>e.value===t);return i?i.label:""}))}openProjectDetailDialog(t){this.dialogService.open(m,{data:{project:t||null,projects:this.projects,venues:this.venueService.load(),type:this.selectValueService.load("Project","Type").pipe((0,r.U)(()=>this.selectValueService.get("Project","Type"))),genre:this.selectValueService.load("Project","Genre").pipe((0,r.U)(()=>this.selectValueService.get("Project","Genre"))),state:this.state},header:this.translate.instant(t?"projects.EDIT_PROJECT":"projects.NEW_PROJECT")}).onClose.pipe((0,a.P)()).subscribe(e=>{!t&&e?this.saveNewProject(e):t&&e&&this.updateProject(e,t)})}openParticipationDialog(t,e){t.stopPropagation(),this.dialogService.open(v,{data:{projectParticipation:this.selectValueService.load("ProjectParticipation","ParticipationStatusInner"),musicianProfiles:this.meService.getProfileMusician(),sections:this.sectionService.load(),id:e},header:this.translate.instant("projects.EDIT_PARTICIPATION")}).onClose.pipe((0,a.P)()).subscribe(t=>{t&&this.meService.putProjectParticipation(t.musicianId,e,{statusId:t.statusId,comment:t.comment}).subscribe(()=>this.notificationsService.success("projects.SET_PARTICIPATION_STATUS"))})}openParticipationListDialog(t,e){t.stopPropagation(),this.dialogService.open(w,{data:{project:e},header:`${this.translate.instant("projects.PARTICIPANTS")}: ${e.title}`})}clear(t){t.clear()}saveNewProject(t){this.projectService.create(t).subscribe(t=>{this.projects=this.projectService.load(),this.notificationsService.success("projects.PROJECT_CREATED")})}updateProject(t,e){this.projectService.update(t).subscribe(()=>{this.projects=this.projectService.load(),this.notificationsService.success("projects.PROJECT_UPDATED")})}};return t.\u0275fac=function(e){return new(e||t)(p.Y36(D.gz),p.Y36(l.xA),p.Y36(c.sK),p.Y36(q.Y),p.Y36(E.T),p.Y36(N.w),p.Y36(O.W),p.Y36(M.P),p.Y36(k.l))},t.\u0275cmp=p.Xpm({type:t,selectors:[["arpa-project-list"]],decls:11,vars:13,consts:[["dataKey","id",3,"value","rows","rowsPerPageOptions","paginator","globalFilterFields"],["dt1",""],["pTemplate","caption"],["pTemplate","header"],["pTemplate","body"],[1,"p-formgroup-inline","p-jc-between"],[1,"p-field","p-col","p-text-right",2,"margin-right","0"],["pButton","","icon","pi pi-plus",3,"label","click"],[1,"p-d-flex"],[1,"p-input-icon-left","p-mr-auto"],[1,"pi","pi-search"],["pInputText","","type","text",3,"placeholder","input"],["pButton","","icon","pi pi-filter-slash",1,"p-button-outlined",3,"label","click"],["pSortableColumn","title"],[1,"p-d-flex","p-jc-between","p-ai-center"],["type","text","field","title","display","menu"],["pSortableColumn","shortTitle"],["type","text","field","shortTitle","display","menu"],["pSortableColumn","venue"],["type","text","field","venue","display","menu"],["pSortableColumn","startDate"],["type","date","field","startDate","display","menu"],["pSortableColumn","endDate"],["type","date","field","endDate","display","menu"],["pSortableColumn","stateId"],["type","text","field","stateId","display","menu"],["pSortableColumn","isCompleted"],["type","boolean","field","isCompleted","display","menu"],[1,"clickable",3,"click"],[1,"p-column-title"],[1,"p-text-lg-center"],["class","pi pi-check-square",4,"ngIf"],[1,"p-text-lg-right"],["pButton","","type","button","class","p-button-rounded p-button-text p-button-icon-only","icon","pi pi-sign-in",3,"label","click",4,"ngIf"],["pButton","","type","button","icon","pi pi-users",1,"p-button-rounded","p-button-text","p-button-icon-only",3,"label","click"],[1,"pi","pi-check-square"],["pButton","","type","button","icon","pi pi-sign-in",1,"p-button-rounded","p-button-text","p-button-icon-only",3,"label","click"]],template:function(t,e){1&t&&(p.TgZ(0,"p-table",0,1),p.ALo(2,"async"),p._uU(3,"> "),p.YNc(4,J,7,6,"ng-template",2),p.YNc(5,$,37,21,"ng-template",3),p.YNc(6,R,43,38,"ng-template",4),p.qZA(),p.TgZ(7,"div",5),p.TgZ(8,"div",6),p.TgZ(9,"button",7),p.NdJ("click",function(){return e.openProjectDetailDialog(null)}),p.ALo(10,"translate"),p.qZA(),p.qZA(),p.qZA()),2&t&&(p.Q6J("value",p.lcZ(2,6,e.projects)||p.DdM(10,Q))("rows",10)("rowsPerPageOptions",p.DdM(11,B))("paginator",!0)("globalFilterFields",p.DdM(12,V)),p.xp6(9),p.MGl("label"," ",p.lcZ(10,8,"projects.NEW_PROJECT"),""))},directives:[j.iA,U.jx,h.Hq,u.o,j.lQ,j.xl,g.O5],pipes:[g.Ov,c.X$,g.uU],styles:["[_nghost-%COMP%]     .p-datatable .p-datatable-tbody>tr>td .p-column-title{display:none}[_nghost-%COMP%]     .action{width:4rem}[_nghost-%COMP%]     .p-datatable-wrapper table{background-color:#1e1e1e}@media screen and (max-width:50rem){[_nghost-%COMP%]     .p-datatable .p-datatable-tfoot>tr>td, [_nghost-%COMP%]     .p-datatable .p-datatable-thead>tr>th{display:none!important}[_nghost-%COMP%]     .p-datatable .p-datatable-tbody>tr>td{text-align:left;display:block;width:100%;float:left;clear:left;border:0}[_nghost-%COMP%]     .p-datatable .p-datatable-tbody>tr>td .p-column-title{padding:.4rem;min-width:30%;display:inline-block;margin:-.4em 1em -.4em -.4rem;font-weight:700}[_nghost-%COMP%]     .p-datatable .p-datatable-tbody>tr>td:last-child{border-bottom:1px solid var(--surface-d)}}"]}),t=(0,o.__decorate)([(0,s.f)()],t),t})()}}]);